using System;
using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace NewTodo.DbMigration
{
    public class DbMigrator
    {
        public static void Migrate(string connectionString)
        {
            var serviceProvider = CreateServices(connectionString);

            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
        }

        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            try
            {
                runner.MigrateUp();
            }
            catch (Exception e)
            {
                throw new Exception( "Database migration failed");
            }
        }

        private static IServiceProvider CreateServices(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new Exception("Connection string argument is missing");

            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(r => r
                    .AddSqlServer()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(Assembly.GetExecutingAssembly()).For.All())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }
    }
}