using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WieEetErMee.Client;
using WieEetErMee.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddBlazoredLocalStorageAsSingleton();
builder.Services.AddSingleton<CurrentUserRepository>();

builder.Services.AddScoped<UserPresenceRepository>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<DayPresenceRepository>();



await builder.Build().RunAsync();
