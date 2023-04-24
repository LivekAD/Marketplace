using Marketplace;
using Marketplace.Hubs;
using Marketplace.DAL;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

//Add JSON file with Data Base settings
IConfigurationBuilder iBuilder = new ConfigurationBuilder();
iBuilder.SetBasePath(Directory.GetCurrentDirectory());
iBuilder.AddJsonFile("dbsettings.json");
IConfigurationRoot _confString = iBuilder.Build();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(_confString.GetConnectionString("DefaultConnection")));


// Add services to the container.
builder.Services.AddControllersWithViews();

/*var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(connection));*/


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
        options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
    });

builder.Services.InitializeRepositories();
builder.Services.InitializeServices();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<ConnectedHub>("/ConnectedHub");

/*app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ConnectedHub>("/ConnectedHub");
    endpoints.MapControllerRoute(
    name: "chat",
    pattern: "chat/{productId}/{user1Id}/{user2Id}",
    defaults: new { controller = "Product", action = "GetProducts" });
    endpoints.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
});*/

app.Run();
