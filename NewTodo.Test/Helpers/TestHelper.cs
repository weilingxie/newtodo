using Microsoft.Extensions.Configuration;

namespace NewTodo.Test.Helpers
{
    public class TestHelper
    {
        private static IConfigurationRoot GetIConfigurationRoot(string outputPath)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(outputPath)
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables();
            
            return configuration.Build();
        }

        public static string GetConnectionString(string outputPath)
        {
            var config = GetIConfigurationRoot(outputPath);
            var connectionString = config["ConnectionStrings:DefaultConnection"];
            return connectionString;
        }
    }
}