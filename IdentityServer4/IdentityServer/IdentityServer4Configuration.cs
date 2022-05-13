using IdentityModel;
using IdentityServer4.Models;

namespace IdentityServer
{
    public class IdentityServer4Configuration
    {
        public static List<ApiResource> Apis => new List<ApiResource>
        {
            new ApiResource("ApiOne")
            {
                 UserClaims = { JwtClaimTypes.Audience},
                 Scopes = new[] { "ApiOne" }
            }
        };

        public static List<Client> Clients => new List<Client>(){
            new Client
            {
                ClientId ="client_id",
                ClientName ="clien_name",
                ClientSecrets =  {new Secret("client_secret".ToSha256() ) },
                //How to access the access token
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                //What this access token will allowed to do 
                AllowedScopes = { "ApiOne"},
                AllowOfflineAccess = true,
            }
        };

        public static List<ApiScope> Scopes => new List<ApiScope>
        {
            new ApiScope{Name = "ApiOne"}
        };
    }

}