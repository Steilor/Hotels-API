using Hotelss.Application.Extensions;
using Hotelss.Infrastructure.Extensions;
using Hotelss.Infrastructure.Seeders;
using Serilog;
using Serilog.Events;

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
                 .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                 .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Information)
                 .WriteTo.Console(outputTemplate: "[{Timestamp:dd-MM HH:mm:ss} {Level:u3}] |{SourceContext}| {NewLine}{Message:lj}{NewLine}{Exception}")
                    ); 

            var app = builder.Build();

            //Add the Seeder
            var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<IHotelSeeder>();
            
            await seeder.Seed();
           

            // Configure the HTTP request pipeline. 
            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
