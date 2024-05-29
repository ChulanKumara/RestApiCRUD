using EmissionBreakdownApi.Data;
using EmissionBreakdownApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmissionBreakdownApi.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection")); // SqlLite used for database
            });

            services.AddCors();
            services.AddScoped<IEmissionBreakdownRepository, EmissionBreakdownRepository>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}
