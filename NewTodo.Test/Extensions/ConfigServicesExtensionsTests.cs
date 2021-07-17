using System;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace NewTodo.Test.Extensions
{
    [Trait("Category", "UnitTest")]
    public class ConfigServicesExtensionsTests
    {
        private readonly IHost host;
        private readonly IServiceProvider services;

        public ConfigServicesExtensionsTests()
        {
            host = Program.CreateHostBuilder(Array.Empty<string>()).Build();
            services = host.Services.CreateScope().ServiceProvider;
        }

        [Fact]
        public void CanFindFluentValidationConfiguration()
        {
            var result = services.GetService<ValidatorConfiguration>();
            Assert.NotNull(result);
        }
    }
}