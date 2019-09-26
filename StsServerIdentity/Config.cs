// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

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
                new IdentityResources.Email()
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("thingsscope")
                {
                    ApiSecrets =
                    {
                        new Secret("thingsscopeSecret".Sha256())
                    },
                    UserClaims = { "role", "admin", "user", "thingsapi" }
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientName = "angularmvcmixedclient",
                    ClientId = "angularmvcmixedclient",
                    ClientSecrets = {new Secret("thingsscopeSecret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    AllowOfflineAccess = true,
                    RedirectUris = { "https://localhost:44341/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:44341/signout-callback-oidc" },
                    AllowedCorsOrigins = new List<string>
                    {
                        "https://localhost:44341"
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "thingsscope",
                        "role"
                    }
                }
            };
        }
    }
}