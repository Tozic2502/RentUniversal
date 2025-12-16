using System.Text.Json.Serialization;
using RentUniversal.Infrastructure.DependencyInjection;
using RentUniversal.Application.Interfaces;
using RentUniversal.Application.Services;
// Add this using if you are using Swashbuckle or NSwag for OpenAPI/Swagger
using Microsoft.OpenApi.Models;

namespace RentUniversal.api
{
    /// <summary>
    /// Application entry point for the RentUniversal API.
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Infrastructure layer (MongoDB, repositories)
            builder.Services.AddInfrastructure(builder.Configuration);


            // Application services
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IItemService, ItemService>();
            builder.Services.AddScoped<IRentalService, RentalService>();
            builder.Services.AddScoped<ILicenseService, LicenseService>();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });


            // Controllers + OpenAPI
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(
                        new JsonStringEnumConverter()
                    );
                });
            builder.Services.AddEndpointsApiExplorer();
            // Replace AddOpenApi() with AddSwaggerGen() if using Swashbuckle
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "RentUniversal API",
                    Version = "v1"
                });

                c.UseAllOfToExtendReferenceSchemas();
            });


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                // Replace UseOpenApi() with UseSwagger() and UseSwaggerUI()
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "RentUniversal API v1");
                });
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthorization();
            app.MapControllers();
            app.Run();

        }
    }
}