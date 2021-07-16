using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using NewTodo.Application.TodoItem.Models;
using NewTodo.Application.TodoItem.Validators;

namespace NewTodo.Extensions
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddFluentValidationServices(this IServiceCollection services)
        {
            services.AddControllers()
                .AddFluentValidation(s => { s.RegisterValidatorsFromAssemblyContaining<NewTodoInputValidator>(); }
                );

            services.AddScoped<IValidator<NewTodoInput>, NewTodoInputValidator>();

            return services;
        }

        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "NewTodo", Version = "v1"}); });

            return services;
        }
    }
}