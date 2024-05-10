namespace Holiday.API.Infrastructures.Cors;

public static class Cors
{
    public static void AddCorsSetting(this IServiceCollection services, IHostEnvironment env)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                builder =>
                {
                    if (env.IsDevelopment() || env.IsEnvironment("Testing"))
                    {
                        builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials();
                    }
                });
        });
    }
}
