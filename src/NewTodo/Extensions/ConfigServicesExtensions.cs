using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using NewTodo.Application.TodoItems.Validators;

namespace NewTodo.Extensions
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddFluentValidationServices(this IServiceCollection services)
        {
            services.AddControllers()
                .AddFluentValidation(s => { s.RegisterValidatorsFromAssemblyContaining<NewTodoInputValidator>(); }
                );

            return services;
        }
    }
}