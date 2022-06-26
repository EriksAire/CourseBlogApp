using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddInfrastructureServices(builder.Configuration);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllersWithViews();

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
}));

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors(cors=>cors.AllowAnyMethod().WithOrigins("http://localhost:3000").AllowAnyHeader().AllowCredentials());

app.UseAuthorization();
app.UseAuthentication();

//TODO: Get rid of Identity server
//app.UseIdentityServer();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/action=Index/{id?}");

app.MapFallbackToFile("index.html");

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//        name:"default",
//        pattern: "{controller=Blog}/{action=Index}/{id?}");
//        endpoints.MapRazorPages();

//});

app.Run();
