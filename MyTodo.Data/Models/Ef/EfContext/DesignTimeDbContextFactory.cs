using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTodo.Data.Models.Ef.EfContext
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TODOLYDbContext>
    {
        public TODOLYDbContext CreateDbContext(string[] args)
        {



            IConfigurationRoot configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json")
                 .Build();

            string connectionString = configuration.GetConnectionString("ConnStr");

            //string connStr = "User ID=postgres;password=1234qqqQ;Host=localhost;port=5432;Database=STRDB";

            var builder = new DbContextOptionsBuilder<TODOLYDbContext>();

            builder.UseNpgsql(connectionString);

            return new TODOLYDbContext(builder.Options);

        }
    }
}
