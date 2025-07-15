using Hotelss.API.Extensions;
using Hotelss.API.Middlewares;
using Hotelss.Application.Extensions;
using Hotelss.Domain.Entities;
using Hotelss.Infrastructure.Extensions;
using Hotelss.Infrastructure.Seeders;
using Serilog;

namespace Hotelss.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.AddPresentation();


            //Send configuration to the extension Method for the configuration of the DbContext
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);


            var app = builder.Build();

            //Add the Seeder
            var scope = app.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<IHotelSeeder>();

            await seeder.Seed();


            // Configure the HTTP request pipeline.
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<RequesttTimeLoggingMiddleware>();

            app.UseSerilogRequestLogging();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

            }

            app.UseHttpsRedirection();

            app.MapGroup("api/identity")
                .WithTags("Identity")
                .MapIdentityApi<User>();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
        catch(Exception ex)
        {
            Log.Fatal(ex, "Application startup failed");
        }
        finally
        {
            Log.CloseAndFlush();
        }
        
    }
}
