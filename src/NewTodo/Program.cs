using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NewTodo.DbMigration;

namespace NewTodo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostBuilder = CreateHostBuilder(args).Build();
            MigrateDatabase(hostBuilder);

            hostBuilder.Run();
        }

        private static void MigrateDatabase(IHost webHostBuilder)
        {
            var configuration = webHostBuilder.Services.GetService(typeof(IConfiguration)) as IConfiguration;
            if (configuration == null) return;

            var connString = Environment.GetEnvironmentVariable("DBCONNECTION");
            DbMigrator.Migrate(connString);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}