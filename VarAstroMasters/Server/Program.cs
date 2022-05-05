global using VarAstroMasters.Shared.Static;
global using VarAstroMasters.Shared.Models;
global using VarAstroMasters.Shared.Responses;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Identity;
global using VarAstroMasters.Server.Services.AuthService;
global using VarAstroMasters.Server.Services.StarService;
global using VarAstroMasters.Server.Services.UserService;
global using VarAstroMasters.Server.Services.DeviceService;
global using VarAstroMasters.Server.Services.LightCurveService;
global using VarAstroMasters.Shared.DTO;
global using VarAstroMasters.Server.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using static VarAstroMasters.Server.Data.SeedRolesAndAdmin;

var builder = WebApplication.CreateBuilder(args);

// Set CORS policy so that the client can send requests to the server
builder.Services.AddCors(options =>
{
    options.AddPolicy(Keywords.CORS_Policy, b => { b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
});

// Connect the server application to the database and create Identity Context
var dbVersion = new MySqlServerVersion(new Version(10, 6, 3));

builder.Services.AddDbContext<DataContext>(options =>
{
    options
        .UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), dbVersion);
});

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IStarService, StarService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddScoped<ILightCurveService, LightCurveService>();

builder.Services.AddDefaultIdentity<User>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<DataContext>();

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

builder.Services.AddRouting(options => options.LowercaseUrls = true);


builder.Services.AddHttpContextAccessor();

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();

    // Use Swagger when app is in development mode
    app.UseSwagger();
    app.UseSwaggerUI();

    // Seed DB in development mode
    var roleManager = builder.Services.BuildServiceProvider().GetService<RoleManager<IdentityRole>>();
    var userManager = builder.Services.BuildServiceProvider().GetService<UserManager<User>>();
    Seed(userManager, roleManager);
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("CORS_Policy");

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();