// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Collections.Generic;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;

namespace StsServerIdentity
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource("dataeventrecordsscope",new []{ "role", "admin", "user", "dataEventRecords", "dataEventRecords.admin" , "dataEventRecords.user" } )
            };
        }

        public static IEnumerable<ApiResource> GetApiResources(IConfigurationSection authSecretsConfiguration)
        {
            var apiSecret = authSecretsConfiguration["ApiSecret"];

            return new List<ApiResource>
            {
                new ApiResource("dataEventRecords")
                {
                    ApiSecrets =
                    {
                        new Secret(apiSecret.Sha256())
                    },
                    Scopes =
                    {
                        new Scope
                        {
                            Name = "dataeventrecords",
                            DisplayName = "Scope for the dataEventRecords ApiResource"
                        }
                    },
                    UserClaims = { "role", "admin", "user", "dataEventRecords", "dataEventRecords.admin", "dataEventRecords.user" }
                }
            };
        }

        public static IEnumerable<Client> GetClients(IConfigurationSection authConfiguration)
        {
            var vueJsApiUrl = authConfiguration["VueJsApiUrl"];
            return new List<Client>
            {
                new Client
                {
                    ClientName = "vuejs_code_client",
                    ClientId = "vuejs_code_client",
                    AccessTokenType = AccessTokenType.Reference,
                    // RequireConsent = false,
                    AccessTokenLifetime = 330,// 330 seconds, default 60 minutes
                    IdentityTokenLifetime = 300,

                    RequireClientSecret = false,
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,

                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = new List<string>
                    {
                        vueJsApiUrl,
                        $"{vueJsApiUrl}/callback.html",
                        $"{vueJsApiUrl}/silent-renew.html"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        $"{vueJsApiUrl}/",
                        $"{vueJsApiUrl}"
                    },
                    AllowedCorsOrigins = new List<string>
                    {
                        $"{vueJsApiUrl}"
                    },
                    AllowedScopes = new List<string>
                    {
                        "openid",
                        "dataEventRecords",
                        "dataeventrecordsscope",
                        "role",
                        "profile",
                        "email"
                    }
                },
            };
        }
    }
}