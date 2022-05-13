using _02IdentityExample.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddDbContext<ApplicationDbContext>(options=> {
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

services.AddIdentity<IdentityUser,IdentityRole>()
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

services.AddAuthorization();

var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => {
        endpoints.MapDefaultControllerRoute(); 
    });



app.Run();
