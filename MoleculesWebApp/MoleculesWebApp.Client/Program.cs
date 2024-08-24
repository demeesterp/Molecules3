using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MoleculesWebApp.Client.Common;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddBlazorBootstrap();
builder.Services.RegisterClientServices(builder.HostEnvironment);
await builder.Build().RunAsync();
