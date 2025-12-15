using RentUniversal.Infrastructure.DependencyInjection;
using RentUniversal.Application.Interfaces;
using RentUniversal.Application.Services;
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


            // Application services (business logic)
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IItemService, ItemService>();
            builder.Services.AddScoped<IRentalService, RentalService>();
            builder.Services.AddScoped<ILicenseService, LicenseService>();
            
            // CORS configuration
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


            // Controllers & Swagger
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RentUniversal API", Version = "v1" });
            });

            var app = builder.Build();

            // Middleware pipeline
            if (app.Environment.IsDevelopment())
            {
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