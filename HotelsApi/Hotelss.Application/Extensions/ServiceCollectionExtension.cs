using FluentValidation;
using FluentValidation.AspNetCore;
using Hotelss.Application.Hotels;
using Microsoft.Extensions.DependencyInjection;

namespace Hotelss.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
          var applicationAssembly  = typeof(ServiceCollectionExtensions).Assembly;
          services.AddScoped<IHotelsService, HotelsService>();

            services.AddAutoMapper(applicationAssembly);

            services.AddValidatorsFromAssembly(applicationAssembly)
                .AddFluentValidationAutoValidation();
        
        }
    }
}
