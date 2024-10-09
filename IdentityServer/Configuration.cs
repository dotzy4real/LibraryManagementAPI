using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.OpenApi.Writers;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Configuration
    {
        // New
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> Apis =>
            new List<ApiScope>
            {
                new ApiScope("library_api", "My Library API")
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource()
                {
                    Name = "library_api_client",
                    Enabled = true,
                    DisplayName = "My Library API Client",
                    Scopes = new List<string> { "library_api" }
                },
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "library_app",
                    ClientSecrets = { new Secret("console_app_secret".ToSha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = 
                    {
                        "library_api"
                    }
                }
            };
    }
}
