using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using FluentValidation;
using MediatR.Registration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewTodo.Application.TodoItem.Commands;
using Xunit;

namespace NewTodo.Test.Extensions
{
    [Trait("Category", "UnitTest")]
    public class DependencyInjectionConfigTests
    {
        [Fact]
        public void CanResolveAllFluentValidators()
        {
            var validators =
                Assembly.GetAssembly(typeof(CreateTodoItemCommand))
                    ?.GetTypes()
                    .Where(t => !t.IsAbstract && IsAssignableToGenericType(t, typeof(IValidator<>)))
                    .SelectMany(t => t.GetInterfaces())
                    .Where(t => t.IsGenericType && !typeof(IEnumerable).IsAssignableFrom(t) && !t.IsOpenGeneric())
                    .ToList();

            TryResolveTypes(validators);
        }

        private static void TryResolveTypes(IEnumerable<Type> types)
        {
            var host = Program.CreateHostBuilder(new string[] { })
                .UseEnvironment("Development").Build();
            var services = host.Services.CreateScope().ServiceProvider;

            var errorBuilder = new StringBuilder();
            foreach (var serviceType in types)
            {
                try
                {
                    services.GetRequiredService(serviceType);
                }
                catch (Exception ex)
                {
                    errorBuilder.AppendLine(ex.Message);
                }
            }

            Assert.Equal("", errorBuilder.ToString());
        }

        private static bool IsAssignableToGenericType(Type givenType, Type genericType)
        {
            var checkType = givenType;
            while (true)
            {
                var interfaceTypes = checkType.GetInterfaces();

                if (interfaceTypes.Any(it => it.IsGenericType && it.GetGenericTypeDefinition() == genericType)
                    || (checkType.IsGenericType && checkType.GetGenericTypeDefinition() == genericType)) return true;

                var baseType = checkType.BaseType;
                if (baseType == null) return false;

                checkType = baseType;
            }
        }
    }
}