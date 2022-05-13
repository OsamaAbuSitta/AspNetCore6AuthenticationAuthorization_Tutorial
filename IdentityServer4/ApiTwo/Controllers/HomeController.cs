using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;

namespace ApiTwo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        
        public async Task<IActionResult> Index()
        {
            //Retrieve access token 
            var serverClient= _httpClientFactory.CreateClient();
            var discoveryDocumen = await serverClient.GetDiscoveryDocumentAsync("https://localhost:7210/");


            if(discoveryDocumen.IsError)
                throw new Exception(discoveryDocumen.Error);

            var tokenResponse = await serverClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discoveryDocumen.TokenEndpoint,
                ClientId = "client_id",
                ClientSecret = "client_secret",
                Scope = "ApiOne"
            });

            var token = tokenResponse.AccessToken;


            // retrieve secret data
            var apiClient = _httpClientFactory.CreateClient();
            apiClient.SetBearerToken(token);

            var response = await apiClient.GetAsync("https://localhost:7130/Secret");

            var content = await response.Content.ReadAsStringAsync();



            return Ok(new
            {
                accessToken = token,
                message = content
            });
        }
    }
}
