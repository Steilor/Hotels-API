using Hotelss.Application.Extensions;
using Hotelss.Infrastructure.Extensions;
using Hotelss.Infrastructure.Seeders;
using Serilog;

namespace Hotelss.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            //Send configuration to the extension Method for the configuration of the DbContext
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);

            // Serilog
            builder.Host.UseSerilog((context, configuration) =>
                 configuration
                    .WriteTo.Console()); ;

            var app = builder.Build();

            //Add the Seeder
            var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<IHotelSeeder>();
            
            await seeder.Seed();
           

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
