using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Server;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication("OAuth")
                .AddJwtBearer("OAuth", config =>
                {
                    config.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidAudience = Constants.Issuer,
                        ValidIssuer = Constants.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.Secret)),
                        ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha256 }
                    };

                    config.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = context => {
                            if (context.Request.Query.ContainsKey("access_token")) {
                                context.Token = context.Request.Query["access_token"];
                            }
                            return Task.CompletedTask;
                        }
                    };

                });

var app = builder.Build();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
