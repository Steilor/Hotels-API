using Hotelss.Domain.Entities;
using Hotelss.Domain.Repositories;
using Hotelss.Infrastructure.Authorization;
using Hotelss.Infrastructure.Persistence;
using Hotelss.Infrastructure.Repositories;
using Hotelss.Infrastructure.Seeders;
using Microsoft.AspNetCore.Identity;
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
            services.AddDbContext<HotelsDbContext>(options => 
                options.UseSqlServer(connectionString)
                    .EnableSensitiveDataLogging());

            services.AddIdentityApiEndpoints<User>()
                .AddRoles<IdentityRole>()
                .AddClaimsPrincipalFactory<HotelsUserClaimsPrincipalFactory>()
                .AddEntityFrameworkStores<HotelsDbContext>();

            services.AddScoped<IHotelSeeder, HotelSeeder>();
            services.AddScoped<IHotelsRepository, HotelsRepository>();
            services.AddScoped<IRoomsRepository, RoomsRepository>();

            services.AddAuthorizationBuilder()
                .AddPolicy("HasNationality", builder => builder.RequireClaim("Nationality"));
        }
    } 
}
