global using VarAstroMasters.Shared.Static;
global using VarAstroMasters.Shared.Models;
global using VarAstroMasters.Shared.Responses;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Identity;
global using VarAstroMasters.Server.Services.AuthService;
global using VarAstroMasters.Server.Services.StarService;
global using VarAstroMasters.Server.Services.UserService;
global using VarAstroMasters.Server.Services.DeviceService;
global using VarAstroMasters.Server.Services.UserStarIdentificationService;
global using VarAstroMasters.Server.Services.ObservatoryService;
global using VarAstroMasters.Server.Services.LightCurveService;
global using VarAstroMasters.Shared.DTO;
global using VarAstroMasters.Server.Data;
global using VarAstroMasters.Shared.Helpers;
global using AutoMapper;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using static VarAstroMasters.Server.Data.SeedRolesAndAdmin;
using Microsoft.AspNetCore.HttpOverrides;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Set CORS policy so that the client can send requests to the server
builder.Services.AddCors(options =>
{
    options.AddPolicy(Keywords.CORS_Policy, b => { b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
});

// Connect the server application to the database and create Identity Context
var dbVersion = new MySqlServerVersion(new Version(
    int.Parse(builder.Configuration.GetSection(Keywords.DB_Version_Major).Value),
    int.Parse(builder.Configuration.GetSection(Keywords.DB_Version_Minor).Value),
    int.Parse(builder.Configuration.GetSection(Keywords.DB_Version_Build).Value)
));

// Register database context (db access layer)
builder.Services.AddDbContext<DataContext>(options =>
{
    options
        .UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), dbVersion);
});

// Register custom services
// This is where you would add NEW services for extension of existing
// system
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IStarService, StarService>();
builder.Services.AddScoped<IUserStarIdentificationService, UserStarIdentificationService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddScoped<IObservatoryService, ObservatoryService>();
builder.Services.AddScoped<ILightCurveService, LightCurveService>();

// Registering identity service that provides 
// providers for tokens, UI, roles, etc.
builder.Services.AddDefaultIdentity<User>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<DataContext>();

// Registering the JWT token bearing scheme
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration[Keywords.JWT_Issuer],
        ValidAudience = builder.Configuration[Keywords.JWT_Issuer],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration[Keywords.JWT_Key]))
    };
});

// HttpContextAccesor is used to verify the bearer tokens
// contained within the HTTP requests
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.KnownProxies.Add(IPAddress.Parse("192.168.100.22"));
});

var app = builder.Build();

// Migrate if app has been ran in production
if (app.Environment.IsProduction())
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<DataContext>();
        db.Database.Migrate();
    }

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();

    // Use Swagger when app is in development mode
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Seed DB with admin user (credentials: admin@example.com, Kappa123!)
var roleManager = builder.Services.BuildServiceProvider().GetService<RoleManager<IdentityRole>>();
var userManager = builder.Services.BuildServiceProvider().GetService<UserManager<User>>();
Seed(userManager, roleManager);

if (app.Environment.IsDevelopment()) app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();
app.Urls.Add("http://192.168.100.22:5000");
app.UseCors("CORS_Policy");

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();