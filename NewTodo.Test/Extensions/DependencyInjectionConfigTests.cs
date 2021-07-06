using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewTodo.Application.TodoItem.Commands;
using MediatR.Registration;
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
            var host = Program.CreateHostBuilder(new string[]{})
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
            while (true)
            {
                var interfaceTypes = givenType.GetInterfaces();

                if (interfaceTypes.Any(it => it.IsGenericType && it.GetGenericTypeDefinition() == genericType))
                {
                    return true;
                }

                if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType) return true;

                var baseType = givenType.BaseType;
                if (baseType == null) return false;

                givenType = baseType;
            }
        }
    }
}