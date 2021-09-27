﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.7.0.0
//      SpecFlow Generator Version:3.7.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace SimpleIdServer.OpenBankingApi.Host.Acceptance.Tests.Features
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.7.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class AccountsFeature : object, Xunit.IClassFixture<AccountsFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private string[] _featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "Accounts.feature"
#line hidden
        
        public AccountsFeature(AccountsFeature.FixtureData fixtureData, SimpleIdServer_OpenBankingApi_Host_Acceptance_Tests_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Features", "Accounts", "\tCheck /accounts endpoint", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        public virtual void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Get accounts (Basic)")]
        [Xunit.TraitAttribute("FeatureTitle", "Accounts")]
        [Xunit.TraitAttribute("Description", "Get accounts (Basic)")]
        public virtual void GetAccountsBasic()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Get accounts (Basic)", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 4
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table4 = new TechTalk.SpecFlow.Table(new string[] {
                            "Type",
                            "Kid",
                            "AlgName"});
                table4.AddRow(new string[] {
                            "SIG",
                            "1",
                            "RS256"});
#line 5
 testRunner.When("build JSON Web Keys, store JWKS into \'jwks\' and store the public keys into \'jwks_" +
                        "json\'", ((string)(null)), table4, "When ");
#line hidden
                TechTalk.SpecFlow.Table table5 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table5.AddRow(new string[] {
                            "token_endpoint_auth_method",
                            "tls_client_auth"});
                table5.AddRow(new string[] {
                            "response_types",
                            "[token,code,id_token]"});
                table5.AddRow(new string[] {
                            "grant_types",
                            "[client_credentials,authorization_code,implicit]"});
                table5.AddRow(new string[] {
                            "scope",
                            "accounts"});
                table5.AddRow(new string[] {
                            "redirect_uris",
                            "[https://localhost:8080/callback]"});
                table5.AddRow(new string[] {
                            "tls_client_auth_san_dns",
                            "firstMtlsClient"});
                table5.AddRow(new string[] {
                            "id_token_signed_response_alg",
                            "PS256"});
                table5.AddRow(new string[] {
                            "token_signed_response_alg",
                            "PS256"});
                table5.AddRow(new string[] {
                            "request_object_signing_alg",
                            "RS256"});
                table5.AddRow(new string[] {
                            "jwks",
                            "$jwks_json$"});
#line 9
 testRunner.And("execute HTTP POST JSON request \'https://localhost:8080/register\'", ((string)(null)), table5, "And ");
#line hidden
#line 22
 testRunner.And("extract JSON from body", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 23
 testRunner.And("extract parameter \'client_id\' from JSON body", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
                TechTalk.SpecFlow.Table table6 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table6.AddRow(new string[] {
                            "X-Testing-ClientCert",
                            "mtlsClient.crt"});
                table6.AddRow(new string[] {
                            "client_id",
                            "$client_id$"});
                table6.AddRow(new string[] {
                            "scope",
                            "accounts"});
                table6.AddRow(new string[] {
                            "grant_type",
                            "client_credentials"});
#line 25
 testRunner.And("execute HTTP POST request \'https://localhost:8080/mtls/token\'", ((string)(null)), table6, "And ");
#line hidden
#line 32
 testRunner.And("extract JSON from body", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 33
 testRunner.And("extract parameter \'access_token\' from JSON body into \'accessToken\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
                TechTalk.SpecFlow.Table table7 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table7.AddRow(new string[] {
                            "Authorization",
                            "Bearer $accessToken$"});
                table7.AddRow(new string[] {
                            "x-fapi-interaction-id",
                            "guid"});
                table7.AddRow(new string[] {
                            "X-Testing-ClientCert",
                            "mtlsClient.crt"});
                table7.AddRow(new string[] {
                            "data",
                            "{ \"permissions\" : [ \"ReadAccountsBasic\" ] }"});
#line 35
 testRunner.And("execute HTTP POST JSON request \'https://localhost:8080/v3.1/account-access-consen" +
                        "ts\'", ((string)(null)), table7, "And ");
#line hidden
#line 42
 testRunner.And("extract JSON from body", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 43
 testRunner.And("extract parameter \'Data.ConsentId\' from JSON body into \'consentId\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 45
 testRunner.And("\'administrator\' confirm consent \'$consentId$\' for accounts \'22289\', with scopes \'" +
                        "accounts\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
                TechTalk.SpecFlow.Table table8 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table8.AddRow(new string[] {
                            "response_type",
                            "id_token code"});
                table8.AddRow(new string[] {
                            "client_id",
                            "$client_id$"});
                table8.AddRow(new string[] {
                            "state",
                            "MTkCNSYlem"});
                table8.AddRow(new string[] {
                            "response_mode",
                            "fragment"});
                table8.AddRow(new string[] {
                            "scope",
                            "accounts"});
                table8.AddRow(new string[] {
                            "redirect_uri",
                            "https://localhost:8080/callback"});
                table8.AddRow(new string[] {
                            "nonce",
                            "nonce"});
                table8.AddRow(new string[] {
                            "claims",
                            "{ id_token: { openbanking_intent_id : { value: \"$consentId$\", essential: true } }" +
                                " }"});
                table8.AddRow(new string[] {
                            "exp",
                            "$tomorrow$"});
                table8.AddRow(new string[] {
                            "aud",
                            "https://localhost:8080"});
#line 47
 testRunner.And("use \'1\' JWK from \'jwks\' to build JWS and store into \'request\'", ((string)(null)), table8, "And ");
#line hidden
                TechTalk.SpecFlow.Table table9 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table9.AddRow(new string[] {
                            "X-Testing-ClientCert",
                            "mtlsClient.crt"});
                table9.AddRow(new string[] {
                            "response_type",
                            "id_token code"});
                table9.AddRow(new string[] {
                            "scope",
                            "accounts"});
                table9.AddRow(new string[] {
                            "client_id",
                            "$client_id$"});
                table9.AddRow(new string[] {
                            "request",
                            "$request$"});
#line 60
 testRunner.And("execute HTTP GET request \'https://localhost:8080/authorization\'", ((string)(null)), table9, "And ");
#line hidden
#line 68
 testRunner.And("extract \'code\' from callback", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
                TechTalk.SpecFlow.Table table10 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table10.AddRow(new string[] {
                            "X-Testing-ClientCert",
                            "mtlsClient.crt"});
                table10.AddRow(new string[] {
                            "client_id",
                            "$client_id$"});
                table10.AddRow(new string[] {
                            "scope",
                            "accounts"});
                table10.AddRow(new string[] {
                            "grant_type",
                            "authorization_code"});
                table10.AddRow(new string[] {
                            "code",
                            "$code$"});
                table10.AddRow(new string[] {
                            "redirect_uri",
                            "https://localhost:8080/callback"});
#line 70
 testRunner.And("execute HTTP POST request \'https://localhost:8080/mtls/token\'", ((string)(null)), table10, "And ");
#line hidden
#line 79
 testRunner.And("extract JSON from body", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 80
 testRunner.And("extract parameter \'access_token\' from JSON body into \'accessToken\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
                TechTalk.SpecFlow.Table table11 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table11.AddRow(new string[] {
                            "Authorization",
                            "Bearer $accessToken$"});
                table11.AddRow(new string[] {
                            "X-Testing-ClientCert",
                            "mtlsClient.crt"});
#line 82
 testRunner.And("execute HTTP GET request \'https://localhost:8080/v3.1/accounts\'", ((string)(null)), table11, "And ");
#line hidden
#line 87
 testRunner.And("extract JSON from body", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 89
 testRunner.Then("HTTP status code equals to \'200\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 90
 testRunner.Then("JSON \'Data.Account[0].AccountId\'=\'22289\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 91
 testRunner.Then("JSON \'Data.Account[0].AccountSubType\'=\'CurrentAccount\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 92
 testRunner.Then("JSON doesn\'t exist \'Data.Account[0].Accounts[0].Identification\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 93
 testRunner.Then("JSON doesn\'t exist \'Data.Account[0].Accounts[0].SecondaryIdentification\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Get accounts (Detail)")]
        [Xunit.TraitAttribute("FeatureTitle", "Accounts")]
        [Xunit.TraitAttribute("Description", "Get accounts (Detail)")]
        public virtual void GetAccountsDetail()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Get accounts (Detail)", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 95
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
                TechTalk.SpecFlow.Table table12 = new TechTalk.SpecFlow.Table(new string[] {
                            "Type",
                            "Kid",
                            "AlgName"});
                table12.AddRow(new string[] {
                            "SIG",
                            "1",
                            "RS256"});
#line 96
 testRunner.When("build JSON Web Keys, store JWKS into \'jwks\' and store the public keys into \'jwks_" +
                        "json\'", ((string)(null)), table12, "When ");
#line hidden
                TechTalk.SpecFlow.Table table13 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table13.AddRow(new string[] {
                            "token_endpoint_auth_method",
                            "tls_client_auth"});
                table13.AddRow(new string[] {
                            "response_types",
                            "[token,code,id_token]"});
                table13.AddRow(new string[] {
                            "grant_types",
                            "[client_credentials,authorization_code,implicit]"});
                table13.AddRow(new string[] {
                            "scope",
                            "accounts"});
                table13.AddRow(new string[] {
                            "redirect_uris",
                            "[https://localhost:8080/callback]"});
                table13.AddRow(new string[] {
                            "tls_client_auth_san_dns",
                            "firstMtlsClient"});
                table13.AddRow(new string[] {
                            "id_token_signed_response_alg",
                            "PS256"});
                table13.AddRow(new string[] {
                            "token_signed_response_alg",
                            "PS256"});
                table13.AddRow(new string[] {
                            "request_object_signing_alg",
                            "RS256"});
                table13.AddRow(new string[] {
                            "jwks",
                            "$jwks_json$"});
#line 100
 testRunner.And("execute HTTP POST JSON request \'https://localhost:8080/register\'", ((string)(null)), table13, "And ");
#line hidden
#line 113
 testRunner.And("extract JSON from body", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 114
 testRunner.And("extract parameter \'client_id\' from JSON body", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
                TechTalk.SpecFlow.Table table14 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table14.AddRow(new string[] {
                            "X-Testing-ClientCert",
                            "mtlsClient.crt"});
                table14.AddRow(new string[] {
                            "client_id",
                            "$client_id$"});
                table14.AddRow(new string[] {
                            "scope",
                            "accounts"});
                table14.AddRow(new string[] {
                            "grant_type",
                            "client_credentials"});
#line 116
 testRunner.And("execute HTTP POST request \'https://localhost:8080/mtls/token\'", ((string)(null)), table14, "And ");
#line hidden
#line 123
 testRunner.And("extract JSON from body", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 124
 testRunner.And("extract parameter \'access_token\' from JSON body into \'accessToken\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
                TechTalk.SpecFlow.Table table15 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table15.AddRow(new string[] {
                            "Authorization",
                            "Bearer $accessToken$"});
                table15.AddRow(new string[] {
                            "x-fapi-interaction-id",
                            "guid"});
                table15.AddRow(new string[] {
                            "X-Testing-ClientCert",
                            "mtlsClient.crt"});
                table15.AddRow(new string[] {
                            "data",
                            "{ \"permissions\" : [ \"ReadAccountsDetail\" ] }"});
#line 126
 testRunner.And("execute HTTP POST JSON request \'https://localhost:8080/v3.1/account-access-consen" +
                        "ts\'", ((string)(null)), table15, "And ");
#line hidden
#line 133
 testRunner.And("extract JSON from body", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 134
 testRunner.And("extract parameter \'Data.ConsentId\' from JSON body into \'consentId\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 136
 testRunner.And("\'administrator\' confirm consent \'$consentId$\' for accounts \'22289\', with scopes \'" +
                        "accounts\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
                TechTalk.SpecFlow.Table table16 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table16.AddRow(new string[] {
                            "response_type",
                            "id_token code"});
                table16.AddRow(new string[] {
                            "client_id",
                            "$client_id$"});
                table16.AddRow(new string[] {
                            "state",
                            "MTkCNSYlem"});
                table16.AddRow(new string[] {
                            "response_mode",
                            "fragment"});
                table16.AddRow(new string[] {
                            "scope",
                            "accounts"});
                table16.AddRow(new string[] {
                            "redirect_uri",
                            "https://localhost:8080/callback"});
                table16.AddRow(new string[] {
                            "nonce",
                            "nonce"});
                table16.AddRow(new string[] {
                            "claims",
                            "{ id_token: { openbanking_intent_id : { value: \"$consentId$\", essential: true } }" +
                                " }"});
                table16.AddRow(new string[] {
                            "exp",
                            "$tomorrow$"});
                table16.AddRow(new string[] {
                            "aud",
                            "https://localhost:8080"});
#line 138
 testRunner.And("use \'1\' JWK from \'jwks\' to build JWS and store into \'request\'", ((string)(null)), table16, "And ");
#line hidden
                TechTalk.SpecFlow.Table table17 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table17.AddRow(new string[] {
                            "X-Testing-ClientCert",
                            "mtlsClient.crt"});
                table17.AddRow(new string[] {
                            "response_type",
                            "id_token code"});
                table17.AddRow(new string[] {
                            "scope",
                            "accounts"});
                table17.AddRow(new string[] {
                            "client_id",
                            "$client_id$"});
                table17.AddRow(new string[] {
                            "request",
                            "$request$"});
#line 151
 testRunner.And("execute HTTP GET request \'https://localhost:8080/authorization\'", ((string)(null)), table17, "And ");
#line hidden
#line 159
 testRunner.And("extract \'code\' from callback", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
                TechTalk.SpecFlow.Table table18 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table18.AddRow(new string[] {
                            "X-Testing-ClientCert",
                            "mtlsClient.crt"});
                table18.AddRow(new string[] {
                            "client_id",
                            "$client_id$"});
                table18.AddRow(new string[] {
                            "scope",
                            "accounts"});
                table18.AddRow(new string[] {
                            "grant_type",
                            "authorization_code"});
                table18.AddRow(new string[] {
                            "code",
                            "$code$"});
                table18.AddRow(new string[] {
                            "redirect_uri",
                            "https://localhost:8080/callback"});
#line 161
 testRunner.And("execute HTTP POST request \'https://localhost:8080/mtls/token\'", ((string)(null)), table18, "And ");
#line hidden
#line 170
 testRunner.And("extract JSON from body", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 171
 testRunner.And("extract parameter \'access_token\' from JSON body into \'accessToken\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
                TechTalk.SpecFlow.Table table19 = new TechTalk.SpecFlow.Table(new string[] {
                            "Key",
                            "Value"});
                table19.AddRow(new string[] {
                            "Authorization",
                            "Bearer $accessToken$"});
                table19.AddRow(new string[] {
                            "X-Testing-ClientCert",
                            "mtlsClient.crt"});
#line 173
 testRunner.And("execute HTTP GET request \'https://localhost:8080/v3.1/accounts\'", ((string)(null)), table19, "And ");
#line hidden
#line 178
 testRunner.And("extract JSON from body", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
#line 180
 testRunner.Then("HTTP status code equals to \'200\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 181
 testRunner.Then("JSON \'Data.Account[0].AccountId\'=\'22289\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 182
 testRunner.Then("JSON \'Data.Account[0].AccountSubType\'=\'CurrentAccount\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 183
 testRunner.Then("JSON \'Data.Account[0].Accounts[0].Identification\'=\'80200110203345\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
#line 184
 testRunner.Then("JSON \'Data.Account[0].Accounts[0].SecondaryIdentification\'=\'00021\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.7.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                AccountsFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                AccountsFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion