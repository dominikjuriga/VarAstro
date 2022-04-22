global using VarAstroMasters.Shared.Models;
global using VarAstroMasters.Shared.Static;
global using VarAstroMasters.Shared.Responses;
global using VarAstroMasters.Client.Services.AuthService;
global using VarAstroMasters.Client.Services.StarService;
global using VarAstroMasters.Client.Services.LightCurveService;
global using VarAstroMasters.Client.Services.UserService;
global using Blazored.LocalStorage;
global using VarAstroMasters.Client.Providers;
global using VarAstroMasters.Shared.DTO;
global using System.Net.Http.Json;
using VarAstroMasters.Client;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();

// Add Custom services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IStarService, StarService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ILightCurveService, LightCurveService>();

builder.Services.AddScoped<AppAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<AppAuthStateProvider>());
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();