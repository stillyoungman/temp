using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using TheBarbershop.Api.Models;
using TheBarbershop.Core.Infrastructure;
using TheBarbershop.Persistence;

namespace TheBarbershop.Api.Services
{
    public static class ConfigureDependenciesExtension
    {
        public static IServiceCollection ConfigureDependencies(this IServiceCollection services)
        {
            services.AddDbContextPool<BarbershopContext>((serviceProvider, builder) =>
            {
                var connString = serviceProvider.GetRequiredService<IOptions<ApplicationConfiguration>>().Value.DatabaseConnectionString;

                builder.UseMySql(connString, sqlOptions =>
                {
                    sqlOptions.ServerVersion(new Version(8, 0, 17), ServerType.MySql);
                });
            });

            services.AddScoped<IDataContext, BarbershopContext>(sp => sp.GetRequiredService<BarbershopContext>());
            services.AddScoped<TokenService>();
            return services;
        }
    }
}
