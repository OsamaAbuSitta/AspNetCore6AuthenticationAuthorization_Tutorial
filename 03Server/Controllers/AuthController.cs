using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Server;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace _03Server.Controllers
{
    public class AuthController : Controller
    {

        [HttpGet]
        public IActionResult Authorize(string response_type,string client_id,string redirect_uri, string scope, string state)
        {
            var query = new QueryBuilder();
            query.Add("redirectUri", redirect_uri);
            query.Add("state", state);

            return View(model:query.ToString());
        }


        [HttpPost]
        public IActionResult Authorize(string username,  string redirect_uri, string state)
        {

            const string code = "4ce73e5f-ddc8-4519-a559-53431ff29dc6";

            var query = new QueryBuilder();
            query.Add("code", code);
            query.Add("state", state);


            return Redirect($"{redirect_uri}{query.ToString()}");
        }


        [HttpPost]
        public async Task<IActionResult> Token(string grant_type, string code, string redirect_uri, string client_id)
        {

            //some mechanism for validating the code 
            //validate the client id 

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "b3732926-41c9-478d-b2a4-839331512a0b"),
                new Claim("granny", "cookie"),
            };


            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.Secret));
            var algorithm = SecurityAlgorithms.HmacSha256;
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, algorithm);


            var tokenObj = new JwtSecurityToken(
                Constants.Issuer,
                Constants.Audiance,
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: signingCredentials
                );


            var token = new JwtSecurityTokenHandler().WriteToken(tokenObj);

            var responseObject = new
            {
                access_token = token,
                token_type = "Bearer"
            };

            var responseJson = JsonConvert.SerializeObject(responseObject);
            var responseBytes = Encoding.UTF8.GetBytes(responseJson);

            await Response.Body.WriteAsync(responseBytes);


            return Redirect(redirect_uri);
        }

    }
}
