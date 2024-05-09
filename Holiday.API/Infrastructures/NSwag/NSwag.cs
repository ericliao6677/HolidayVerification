using System.Runtime.CompilerServices;

namespace Holiday.API.Infrastructures.NSwag;

public static class NSwag
{
    public static void AddNSwag(this IServiceCollection services, IHostEnvironment env)
    {
        services.AddOpenApiDocument(
            (setting, provider) =>
            {
                setting.Title = $"HolidayVerification API ({env.EnvironmentName})";
                setting.Version = "v1";
            }
        );
    }
}

