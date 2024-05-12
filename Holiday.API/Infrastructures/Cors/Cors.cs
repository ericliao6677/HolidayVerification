namespace Holiday.API.Infrastructures.Cors;

public static class Cors
{
    public static void AddCorsSetting(this IServiceCollection services, IHostEnvironment env)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                policy =>
                {
                    if (env.IsDevelopment())
                    {
                        policy.SetIsOriginAllowed(origin => new Uri(origin).Host == "127.0.0.1")
                                .AllowAnyHeader()
                                .AllowAnyMethod()
                                .AllowCredentials();                      
                    }
                });
        });
    }
}
