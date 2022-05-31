global using VarAstroMasters.Client.Providers;
global using VarAstroMasters.Client.Services.AuthService;
global using VarAstroMasters.Client.Services.DeviceService;
global using VarAstroMasters.Client.Services.LightCurveService;
global using VarAstroMasters.Client.Services.ObservatoryService;
global using VarAstroMasters.Client.Services.UserStarIdentificationService;
global using VarAstroMasters.Client.Services.StarService;
global using VarAstroMasters.Client.Services.UserService;
global using VarAstroMasters.Shared.CompositeKeys;
global using VarAstroMasters.Shared.DTO;
global using VarAstroMasters.Shared.Helpers;
global using VarAstroMasters.Shared.Models;
global using VarAstroMasters.Shared.Responses;
global using VarAstroMasters.Shared.Static;
global using Blazored.LocalStorage;
global using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using VarAstroMasters.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Add interoperability for the browsers Local Storage
builder.Services.AddBlazoredLocalStorage();

// Mud Blazor service (frontent packages)
builder.Services.AddMudServices();

// ASP.NET Authorization service
builder.Services.AddAuthorizationCore();

// Add Custom Services.
// This is where you would register services in case you wanted to 
// develop this further
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IStarService, StarService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserStarIdentificationService, UserStarIdentificationService>();
builder.Services.AddScoped<IDeviceService, DeviceService>();
builder.Services.AddScoped<IObservatoryService, ObservatoryService>();
builder.Services.AddScoped<ILightCurveService, LightCurveService>();

builder.Services.AddScoped<AppAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider =>
    provider.GetRequiredService<AppAuthStateProvider>());
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();