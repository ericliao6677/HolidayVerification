using Holiday.API.Infrastructures.Database;
using Holiday.API.Repositories.Implement;
using Holiday.API.Repositories.Interface;
using Holiday.API.Services.Implement;
using Holiday.API.Services.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Holiday.API.Infrastructures.DependecyInjection
{
    public static class DependecyInjection
    {

        public static void AddInfrastructure(this IServiceCollection service)
        {
            service.AddScoped<DbConnectionHelper>();
            service.AddScoped<IHolidayRepository, HolidayRepository>();
            service.AddScoped<IHolidayService, HolidayService>();           
        }
    }
}
