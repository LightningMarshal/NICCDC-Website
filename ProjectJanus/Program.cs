using ProjectJanus.Middlewares;
using ProjectJanus.Models;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<Security>(config.GetSection("Security")); // Allows for secrets to be set

builder.Services.AddDistributedMemoryCache();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.Strict;
});

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".User.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // IMPORTANT DO NOT REMOVE - This allows all files under wwwRoot to be referenced.

app.UseRouting();

app.UseAuthorization();

app.UseCookiePolicy();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=NICCDC}/{action=Home}/{id?}");

// Custom Middleware:
app.UseMiddleware<CookieMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseMiddleware<TestingMiddleware>();
}

app.Run();