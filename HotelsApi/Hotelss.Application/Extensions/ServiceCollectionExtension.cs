using Hotelss.Application.Hotels;
using Hotelss.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotelss.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
          services.AddScoped<IHotelsService, HotelsService>();

            services.AddAutoMapper(typeof(ServiceCollectionExtension).Assembly);

        
        }
    }
}
