﻿// Copyright (c) SimpleIdServer. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.
using MongoDB.Bson;
using MongoDB.Driver;
using SimpleIdServer.Scim.Domain;
using SimpleIdServer.Scim.Persistence.MongoDB.Models;
using System.Collections.Generic;
using System.Linq;

namespace SimpleIdServer.Scim.Persistence.MongoDB.Extensions
{
    public static class MongoDbClientExtensions
	{
		public static void EnsureMongoDbSCIMDatabaseIsCreated(
			MongoDbOptions options,
			List<SCIMSchema> initialSchemas,
			List<SCIMAttributeMapping> initialAttributeMapping
			)
		{
			var mongoClient = new MongoClient(options.ConnectionString);
			var db = mongoClient.GetDatabase(options.Database);
			EnsureCollectionIsCreated<SCIMRepresentationModel>(db, options.CollectionRepresentations);
			EnsureCollectionIsCreated<ProvisioningConfiguration>(db, options.CollectionProvisioningLst);
			var schemasCollection = EnsureCollectionIsCreated<SCIMSchema>(db, options.CollectionSchemas);
			var mappingsCollection = EnsureCollectionIsCreated<SCIMAttributeMapping>(db, options.CollectionMappings);
			var query = schemasCollection.AsQueryable();
			if (query.Count() == 0)
			{
				if (initialSchemas != null)
				{
					schemasCollection.InsertMany(initialSchemas);
				}
				else
				{
					var schemas = new List<SCIMSchema>
					{
						SCIMConstants.StandardSchemas.GroupSchema,
						SCIMConstants.StandardSchemas.UserSchema
                    };
					schemasCollection.InsertMany(schemas);
				}
			}

			if (mappingsCollection.AsQueryable().Count() == 0)
			{
				if (initialAttributeMapping != null)
				{
					mappingsCollection.InsertMany(initialAttributeMapping);
				}
				else
                {
					mappingsCollection.InsertMany(SCIMConstants.StandardAttributeMapping);
				}
            }
		}

		private static IMongoCollection<T> EnsureCollectionIsCreated<T>(IMongoDatabase db, string name)
		{
			if (!CollectionExists(db, name))
			{
				db.CreateCollection(name);
			}

			return db.GetCollection<T>(name);
		}

		private static bool CollectionExists(IMongoDatabase db, string collectionName)
		{
			var filter = new BsonDocument("name", collectionName);
			var collections = db.ListCollections(new ListCollectionsOptions { Filter = filter });
			return collections.Any();
		}
	}
}
