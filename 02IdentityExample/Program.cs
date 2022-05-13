using _02IdentityExample.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddDbContext<ApplicationDbContext>(options=> {
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

services.AddIdentity<IdentityUser,IdentityRole>( configuration=> {
        configuration.Password.RequireDigit = false;
        configuration.Password.RequireNonAlphanumeric = false;
        configuration.Password.RequireUppercase = false;
        configuration.Password.RequireLowercase = false;
        configuration.Password.RequireDigit = false;
        configuration.Password.RequiredUniqueChars = 1;
        configuration.Password.RequiredLength = 6;
    })
    // Add bredge 
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

// 📽️📹 10


services.AddControllersWithViews();
//services.AddAuthentication("CookieAuth")
//        .AddCookie("CookieAuth",config=>
//        {
//            config.Cookie.Name = "Grandmas.Cookie";
//            config.LoginPath = "/Login";
//        });

//services.AddAuthentication();

services.AddAuthorization();

var app = builder.Build();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => {
        endpoints.MapDefaultControllerRoute(); 
    });



app.Run();
