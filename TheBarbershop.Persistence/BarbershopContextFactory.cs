using System;
using System.Data.Common;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MySql.Data.MySqlClient;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace TheBarbershop.Persistence
{
    public class BarbershopContextFactory: IDesignTimeDbContextFactory<BarbershopContext>
    {
        public BarbershopContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<BarbershopContext>();
            var cb = new MySqlConnectionStringBuilder();

            cb.Server = "127.0.0.1";
            cb.UserID = "root";
            cb.Port = 3306;
            cb.Password = "e573gQURrALSfhNL";
            cb.Database = "barbershop";

            builder.UseMySql(cb.ConnectionString, sqlOptions =>
            {
                sqlOptions.ServerVersion(new Version(8, 0, 17), ServerType.MySql);
            });
            return new BarbershopContext(builder.Options);
        }
    }
}