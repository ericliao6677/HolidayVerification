using Holiday.API.Infrastructures.Cors;
using Holiday.API.Infrastructures.DependecyInjection;
using Holiday.API.Infrastructures.ExceptionHandler;
using Holiday.API.Infrastructures.Filter;
using Holiday.API.Infrastructures.JWTToken;
using Holiday.API.Infrastructures.Logging;
using Holiday.API.Infrastructures.NSwag;
using Microsoft.Extensions.Options;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;
var config = builder.Configuration;
SeriLogHelper.ConfigureSerilLogger(config);


try
{
    //Add services to the container.
    //builder.Services.AddControllers()
    //    .ConfigureApiBehaviorOptions(option =>
    //    {
    //        option.InvalidModelStateResponseFactory = context => BadRequestExceptionHandler.TryHandler(context);
    //    });

    builder.Services.AddControllers(options => options.AddFilter())
        .ConfigureApiBehaviorOptions(options =>
        { 
            options.SuppressModelStateInvalidFilter = true; 
        });


    // Serillog
    builder.Services.AddSerilog();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    //builder.Services.AddEndpointsApiExplorer();
    //builder.Services.AddSwaggerGen();


    // Add OpenAPI
    builder.Services.AddNSwag(env);

    // DI
    builder.Services.AddInfrastructure();

    // AutoMapper
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    // Error Handler
    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
    builder.Services.AddProblemDetails();

    // JWT
    builder.Services.AddJWTTokenConfigurator(config);


    // Add cors
    //builder.Services.AddCorsSetting(env);

    //cache
    builder.Services.AddMemoryCache();


    var app = builder.Build();

    // serve OpenAPI/Swagger documents
    app.UseOpenApi();

    // serve Swagger UI
    app.UseSwaggerUi();

    // serve ReDoc UI
    app.UseReDoc((config) => config.Path = "/redoc");

    //Logging
    app.UseMiddleware<RequestResponseLoggingMiddleware>();
    app.UseSerilogRequestLogging(options => options.EnrichDiagnosticContext = SeriLogHelper.EnrichFromRequest);

    // Error Handler
    app.UseExceptionHandler();


    app.UseHttpsRedirection();

    //app.UseCors();

    //先驗證再授權
    //app.UseMiddleware<CheckblacklistMiddleware>();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{
    Log.Error(ex, "Something went wrong");
}
finally
{
    Log.CloseAndFlush();
}
