using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
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
            services.AddDbContext<TodoDbContext>(options => options.UseInMemoryDatabase(databaseName: "TodoDb"));
        }
    }
}