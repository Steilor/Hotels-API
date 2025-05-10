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
          services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

            services.AddAutoMapper(applicationAssembly);

            services.AddValidatorsFromAssembly(applicationAssembly)
                .AddFluentValidationAutoValidation();
        
        }
    }
}
