using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProyectoLuisOrellana.Infrastructure.DAL.RepositoryManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoLuisOrellana.Infrastructure.DAL.DbConecction
{
    public static class ConfigureConnection
    {
        public static void ConfigureSqlContextManager(this IServiceCollection services, IConfiguration config)
        {          
            services.AddDbContext<DbContextPrueba>();
        }

        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, ProyectoLuisOrellana.Infrastructure.DAL.RepositoryManager.RepositoryManager>();

        }
    }
}
