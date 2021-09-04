using Microsoft.Extensions.Configuration;

namespace NewTodo.IntegrationTest.Helpers
{
    public static class TestHelper
    {
        private static IConfigurationRoot GetIConfigurationRoot(string outputPath)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(outputPath)
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables();

            return configuration.Build();
        }

        public static ServerOptions GetServerOptions(string outputPath)
        {
            var config = GetIConfigurationRoot(outputPath);
            var serverOptions = config.GetSection("Server").Get<ServerOptions>();

            return serverOptions;
        }
    }
}