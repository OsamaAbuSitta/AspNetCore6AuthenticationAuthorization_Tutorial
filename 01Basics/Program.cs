var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication("CookieAuth")
                .AddCookie("CookieAuth", config =>
                 {
                     config.Cookie.Name = "Grandmas.Cookie";
                     config.LoginPath = "/Login";
                 });

builder.Services.AddAuthorization();


var app = builder.Build();



app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});
//app.MapGet("/", () => "Hello World!");



app.Run();
