using System;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NewTodo.Infrastructure;
using Xunit;

namespace NewTodo.Test.Extensions
{
    [Trait("Category", "UnitTest")]
    public class ConfigServicesExtensionsTests
    {
        private readonly IServiceProvider _services;

        public ConfigServicesExtensionsTests()
        {
            var host = Program.CreateHostBuilder(Array.Empty<string>()).Build();
            _services = host.Services.CreateScope().ServiceProvider;
        }

        [Fact]
        public void CanFindFluentValidationConfiguration()
        {
            var result = _services.GetService<ValidatorConfiguration>();
            Assert.NotNull(result);
        }

        [Fact]
        public void CanFindITodoRepository()
        {
            var result = _services.GetService<ITodoRepository>();
            Assert.NotNull(result);
        }

        [Fact]
        public void CanFindDbContext()
        {
            var result = _services.GetService<TodoDbContext>();
            Assert.NotNull(result);
        }
    }
}