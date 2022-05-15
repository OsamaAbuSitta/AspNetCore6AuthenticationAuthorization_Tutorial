
// OAuth Specification
// https://datatracker.ietf.org/doc/html/rfc6749

using Client;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(config =>
                    {
                        // check the cookie
                        config.DefaultAuthenticateScheme = "ClientCookie";
                        //when we sign in 
                        config.DefaultSignInScheme = "ClientCookie";
                        // use this to check if we are allowed to do something 
                        config.DefaultChallengeScheme = "OAuthServer";
                    })
                .AddCookie("ClientCookie")
                .AddOAuth("OAuthServer", config=>
                {
                    config.ClientId = "client_id";
                    config.ClientSecret = "client_secret";
                    config.CallbackPath = "/oauth/callback";
                    config.AuthorizationEndpoint = "https://localhost:7011/auth/authorize";
                    config.TokenEndpoint= "https://localhost:7011/auth/token";
                    config.SaveTokens = true;
                    config.Events = new OAuthEvents
                    {
                        OnCreatingTicket = context =>
                        {
                            var bytes = Convert.FromBase64String(context.AccessToken.Split(".")[1]);
                            var jsonPayload = Encoding.UTF8.GetString(bytes);

                            var claims = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonPayload);

                            foreach (var claim in claims)
                            {
                                context.Identity.AddClaim(new Claim(claim.Key, claim.Value));
                            }

                            return Task.CompletedTask;
                        }

                    };
                });

var app = builder.Build();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(config=> {
    config.MapDefaultControllerRoute();
});



app.Run();
