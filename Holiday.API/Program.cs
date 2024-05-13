using Holiday.API.Infrastructures.Cors;
using Holiday.API.Infrastructures.DependecyInjection;
using Holiday.API.Infrastructures.Logging;
using Holiday.API.Infrastructures.NSwag;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;
var config = builder.Configuration;
SeriLogHelper.ConfigureSerilLogger(config);


try
{
    // Add services to the container.
    builder.Services.AddControllers();

    // Serillog
    builder.Services.AddSerilog();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();


    // add OpenAPI
    builder.Services.AddNSwag(env);

    //AutoMapper
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    //DI
    builder.Services.AddInfrastructure();

    //add cors
    builder.Services.AddCorsSetting(env);


    var app = builder.Build();


    app.UseOpenApi();
    app.UseSwaggerUi();
    app.UseReDoc((config) => config.Path = "/redoc");

    //Logging
    app.UseMiddleware<RequestResponseLoggingMiddleware>();
    app.UseSerilogRequestLogging(options => options.EnrichDiagnosticContext = SeriLogHelper.EnrichFromRequest);


    app.UseHttpsRedirection();

    app.UseCors();

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
