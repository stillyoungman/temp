using System;
using System.Data.Common;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.Extensions.Configuration.Json;
using System.IO;

namespace TheBarbershop.Persistence
{
    public class BarbershopContextFactory: IDesignTimeDbContextFactory<BarbershopContext>
    {
        const string configFileName = "appsettings.Development.json";
        const string connectionStringSectionName = "databaseConnectionString";
        public BarbershopContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<BarbershopContext>();
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile(Path.Combine(".", configFileName));
            var configuration = configurationBuilder.Build();
            //var cb = new MySqlConnectionStringBuilder
            //{
            //    Server = "178.62.204.251",
            //    UserID = "root",
            //    Port = 3306,
            //    Password = "e573gQURrALSfhNL",
            //    Database = "barbershop"
            //};


            builder.UseMySql(configuration[connectionStringSectionName], sqlOptions =>
            {
                sqlOptions.ServerVersion(new Version(8, 0, 17), ServerType.MySql);
            });
            return new BarbershopContext(builder.Options);
        }
    }
}