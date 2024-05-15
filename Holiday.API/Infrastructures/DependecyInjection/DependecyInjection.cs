using Holiday.API.Infrastructures.Database;
using Holiday.API.Infrastructures.JWTToken;
using Holiday.API.Repositories.Implement;
using Holiday.API.Repositories.Interface;
using Holiday.API.Services.Implement;
using Holiday.API.Services.Interface;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;

namespace Holiday.API.Infrastructures.DependecyInjection
{
    public static class DependecyInjection
    {

        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<DbConnectionHelper>();
            services.AddScoped<IHolidayRepository, HolidayRepository>();
            services.AddScoped<IHolidayService, HolidayService>();

            services.AddScoped<JWTTokenHelper>();
        }
    }
}
