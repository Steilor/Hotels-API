using Hotelss.Domain.Entities;
using Hotelss.Domain.Interfaces;
using Hotelss.Domain.Repositories;
using Hotelss.Infrastructure.Authorization;
using Hotelss.Infrastructure.Authorization.Requirements;
using Hotelss.Infrastructure.Authorization.Services;
using Hotelss.Infrastructure.Persistence;
using Hotelss.Infrastructure.Repositories;
using Hotelss.Infrastructure.Seeders;
using Microsoft.AspNetCore.Authorization;
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
                .AddPolicy(PolicyNames.HasNationality, builder => builder.RequireClaim(AppClaimTypes.Nationality, "German", "Polish"))
                .AddPolicy(PolicyNames.AtLeast20,
                     builder => builder.AddRequirements(new MinimumAgeRequirement(20)))
                .AddPolicy(PolicyNames.CreatedAtleast2Hotels,
                     builder => builder.AddRequirements(new CreatedMultipleHotelsRequirement(2)));
                

            services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
            services.AddScoped<IAuthorizationHandler, CreatedMultipleHotelsRequirementHandler>();
            services.AddScoped<IHotelAuthorizationService, HotelAuthorizationService>();


        }
    } 
}
