using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Practics.Synchronizer.DAL.Context;

namespace Practics.Synchronizer.Extensions
{
    public static class EntityFrameworkConfigurationExtensions
    {
        public static void AddEntityFrameworkCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>
            (
                opt=>
                    opt.UseNpgsql
                    (
                        configuration.GetConnectionString("PostgreSQL")
                    ), 
                ServiceLifetime.Transient
            );
        }
    }
}