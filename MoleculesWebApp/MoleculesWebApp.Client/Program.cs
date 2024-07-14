using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MoleculesWebApp.Client.Common;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.RegisterServices(builder.HostEnvironment);
await builder.Build().RunAsync();
