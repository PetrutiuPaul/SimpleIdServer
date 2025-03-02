﻿// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SimpleIdServer.Jwt;
using SimpleIdServer.Jwt.Extensions;
using SimpleIdServer.Scim.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace SimpleIdServer.Scim.MongoDb.Startup
{
    public class Startup
    {
        private const string CONNECTION_STRING = "mongodb://localhost:27017";
        private const string DATABASE_NAME = "scim";

        public const string REPRESENTATIONS = "representations";
        public const string SCHEMAS = "schemas";
        public const string MAPPINGS = "mappings";
        public const bool SUPPORT_TRANSACTION = false;

        public Startup(IWebHostEnvironment env, IConfiguration configuration) 
        {
            Env = env;
            Configuration = configuration;
        }

        public IWebHostEnvironment Env { get; }
        public IConfiguration Configuration { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            var json = File.ReadAllText("oauth_puk.txt");
            var dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            var rsaParameters = new RSAParameters
            {
                Modulus = dic.TryGet(RSAFields.Modulus),
                Exponent = dic.TryGet(RSAFields.Exponent)
            };
            var oauthRsaSecurityKey = new RsaSecurityKey(rsaParameters);
            services.AddMvc(o =>
            {
                o.EnableEndpointRouting = false;
                o.AddSCIMValueProviders();
            }).AddNewtonsoftJson(o => { });
            services.AddLogging();
            services.AddAuthorization(opts => opts.AddDefaultSCIMAuthorizationPolicy());
            services.AddAuthentication(SCIMConstants.AuthenticationScheme)
                .AddJwtBearer(SCIMConstants.AuthenticationScheme, cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = "https://localhost:60000",
                        ValidAudiences = new List<string>
                        {
                            "scimClient", "gatewayClient"
                        },
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = oauthRsaSecurityKey
                    };
                });
            services.AddSIDScim(_ =>
            {
                _.IgnoreUnsupportedCanonicalValues = false;
            });
            var basePath = Path.Combine(Env.ContentRootPath, "Schemas");
            var userSchema = SCIMSchemaExtractor.Extract(Path.Combine(basePath, "UserSchema.json"), SCIMConstants.SCIMEndpoints.User, true);
            var eidUserSchema = SCIMSchemaExtractor.Extract(Path.Combine(basePath, "EIDUserSchema.json"), SCIMConstants.SCIMEndpoints.User);
            var groupSchema = SCIMSchemaExtractor.Extract(Path.Combine(basePath, "GroupSchema.json"), SCIMConstants.SCIMEndpoints.Group, true);
            userSchema.SchemaExtensions.Add(new SCIMSchemaExtension
            {
                Id = Guid.NewGuid().ToString(),
                Schema = "urn:ietf:params:scim:schemas:extension:eid:2.0:User"
            });
            var schemas = new List<SCIMSchema>
            {
                userSchema,
                groupSchema,
                eidUserSchema
            };
            services.AddScimStoreMongoDB(opt =>
            {
                opt.ConnectionString = CONNECTION_STRING;
                opt.Database = DATABASE_NAME;
                opt.CollectionMappings = MAPPINGS;
                opt.CollectionRepresentations = REPRESENTATIONS;
                opt.CollectionSchemas = SCHEMAS;
                opt.SupportTransaction = SUPPORT_TRANSACTION;
            }, schemas,
            new List<SCIMAttributeMapping>
            {
                new SCIMAttributeMapping
                {
                    Id = Guid.NewGuid().ToString(),
                    SourceAttributeId = userSchema.Attributes.First(a => a.Name == "groups").Id,
                    SourceResourceType = SCIMConstants.StandardSchemas.UserSchema.ResourceType,
                    SourceAttributeSelector = "groups",
                    TargetResourceType = SCIMConstants.StandardSchemas.GroupSchema.ResourceType,
                    TargetAttributeId = groupSchema.Attributes.First(a => a.Name == "members").Id
                },
                new SCIMAttributeMapping
                {
                    Id = Guid.NewGuid().ToString(),
                    SourceAttributeId = groupSchema.Attributes.First(a => a.Name == "members").Id,
                    SourceResourceType = SCIMConstants.StandardSchemas.GroupSchema.ResourceType,
                    SourceAttributeSelector = "members",
                    TargetResourceType = SCIMConstants.StandardSchemas.UserSchema.ResourceType,
                    TargetAttributeId = userSchema.Attributes.First(a => a.Name == "groups").Id
                }
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseMvc();
        }

    }
}