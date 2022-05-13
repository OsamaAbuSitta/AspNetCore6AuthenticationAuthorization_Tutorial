using IdentityServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddIdentityServer()
    .AddInMemoryApiResources(IdentityServer4Configuration.Apis)
    .AddInMemoryClients(IdentityServer4Configuration.Clients)
    .AddInMemoryApiScopes(IdentityServer4Configuration.Scopes)
    .AddDeveloperSigningCredential();

//https://localhost:7210/.well-known/openid-configuration




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.UseIdentityServer();

app.Run();
