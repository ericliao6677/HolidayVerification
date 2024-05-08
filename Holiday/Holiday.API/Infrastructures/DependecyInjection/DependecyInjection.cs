using Holiday.API.Infrastructures.Database;
using Microsoft.Extensions.DependencyInjection;

namespace Holiday.API.Infrastructures.DependecyInjection
{
    public static class DependecyInjection
    {
        
        public static void AddInfrastructure(this IServiceCollection service)
        {
            service.AddScoped<DbConnectionHelper>();
           
        }
    }
}
