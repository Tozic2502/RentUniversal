using RentUniversal.Infrastructure.DependencyInjection;
using RentUniversal.Application.Interfaces;
using RentUniversal.Application.Services;

namespace RentUniversal.api
{
    public class Program
    {

        public static void Main(string[] args)

        {
            var builder = WebApplication.CreateBuilder(args);

            // earlier in builder setup:
            builder.Services.AddInfrastructure(builder.Configuration);

            // Register Application services that depend on repository implementations:
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IItemService, ItemService>();
            builder.Services.AddScoped<IRentalService, RentalService>();
            builder.Services.AddScoped<ILicenseService, LicenseService>();

            
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
