using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace NewTodo.Extensions
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddControllers().AddFluentValidation(s =>
                {
                    s.RegisterValidatorsFromAssemblyContaining<Startup>();
                }
            );
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "NewTodo", Version = "v1"}); });
            
            return services;
        }
    }
}