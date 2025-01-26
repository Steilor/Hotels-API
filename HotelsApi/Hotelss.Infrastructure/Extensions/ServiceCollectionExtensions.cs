using Hotelss.Domain.Repositories;
using Hotelss.Infrastructure.Persistence;
using Hotelss.Infrastructure.Repositories;
using Hotelss.Infrastructure.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hotelss.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("HotelsDb");
            services.AddDbContext<HotelsDbContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IHotelSeeder, HotelSeeder>();
            services.AddScoped<IHotelsRepository, HotelsRepository>();
        }
    } 
}
