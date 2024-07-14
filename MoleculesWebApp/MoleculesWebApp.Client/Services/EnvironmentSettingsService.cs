using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MoleculesWebApp.Client.Common;

namespace MoleculesWebApp.Client.Services
{
    public static class EnvironmentSettingsService
    {
        public static string GetApiBasePath(this IWebAssemblyHostEnvironment hostingEnvironment)
        {
            if (hostingEnvironment.Environment == Environments.Development.ToString())
            {
                return "https://localhost:44341/";
            }
            throw new NotImplementedException($"Environment {hostingEnvironment.Environment} is not supported");
        }
    }
}
