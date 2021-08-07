using System;
using System.Data;
using FluentValidation.AspNetCore;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using NewTodo.Application.TodoItems.Validators;
using NewTodo.Infrastructure;

namespace NewTodo.Extensions
{
    public static class DependencyInjectionConfig
    {
        public static void AddFluentValidationServices(this IServiceCollection services)
        {
            services.AddControllers()
                .AddFluentValidation(s => { s.RegisterValidatorsFromAssemblyContaining<NewTodoInputValidator>(); }
                );
        }

        public static void AddTodoServices(this IServiceCollection services)
        {
            services.AddTransient<ITodoRepository, TodoRepository>();
            services.AddScoped<IDbConnection>(provider =>
                new SqlConnection(Environment.GetEnvironmentVariable("DBCONNECTION")));
        }
    }
}