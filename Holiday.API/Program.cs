using Holiday.API.Infrastructures.DependecyInjection;
using Holiday.API.Infrastructures.NSwag;

var builder = WebApplication.CreateBuilder(args);
var env = builder.Environment;


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add OpenAPI
builder.Services.AddNSwag(env);


//AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//DI
builder.Services.AddInfrastructure();

var app = builder.Build();


app.UseOpenApi();
app.UseSwaggerUi();
app.UseReDoc((config) => config.Path = "/redoc");
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
