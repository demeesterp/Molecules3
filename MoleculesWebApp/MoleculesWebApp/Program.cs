using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Molecules.Core.Data;
using MoleculesWebApp.Components;
using MoleculesWebApp.Extensions;
using MoleculesWebApp.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);
builder.Configuration.AddEnvironmentVariables();

var configuration = builder.Configuration!;

builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddDbContext<MoleculesDbContext>(options =>
                  options.UseNpgsql(configuration.GetConnectionString("ConnectionString")));

builder.Services.AddMoleculesServices(configuration);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Molecules API", Version = "v1" });
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// Add global exception handler
app.Use(async (context, next) =>
{
    try
    {
        await next(context);
    }
    catch (Exception ex)
    {
        await GlobalExceptionHandler.HandleExceptionAsync(context, ex);    
    }
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
// specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Molecules API V1"));

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(MoleculesWebApp.Client._Imports).Assembly);

app.RegisterMoleculesEndpoints();

app.Run();


