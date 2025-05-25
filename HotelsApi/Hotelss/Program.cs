using Hotelss.API.Middlewares;
using Hotelss.Application.Extensions;
using Hotelss.Domain.Entities;
using Hotelss.Infrastructure.Extensions;
using Hotelss.Infrastructure.Seeders;
using Serilog;
using Microsoft.OpenApi.Models;

namespace Hotelss.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        //Add Swagger
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });
        });

        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddScoped<ErrorHandlingMiddleware>();
        builder.Services.AddScoped<RequesttTimeLoggingMiddleware>();

        //Send configuration to the extension Method for the configuration of the DbContext
        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(builder.Configuration);

        // Serilog
        builder.Host.UseSerilog((context, configuration) =>
             configuration.ReadFrom.Configuration(context.Configuration)               
        ); 
        
        var app = builder.Build();

        //Add the Seeder
        var scope = app.Services.CreateScope();
        var seeder = scope.ServiceProvider.GetRequiredService<IHotelSeeder>();
        
        await seeder.Seed();
       

        // Configure the HTTP request pipeline.
        app.UseMiddleware<ErrorHandlingMiddleware>();
        app.UseMiddleware <RequesttTimeLoggingMiddleware>();

        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();

        }

        app.UseHttpsRedirection();

        app.MapIdentityApi<User>();

        app.UseAuthorization();
          

        app.MapControllers();

        app.Run();
    }
}
