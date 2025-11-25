using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentUniversal.Application.Interfaces;
using RentUniversal.Infrastructure.Configuration;
using RentUniversal.Infrastructure.Data;
using RentUniversal.Infrastructure.Repositories;

namespace RentUniversal.Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Bind settings
            var mongoSettings = new MongoDbSettings();
            configuration.GetSection("MongoDb").Bind(mongoSettings);

            services.AddSingleton<IMongoDbSettings>(mongoSettings);
            services.AddSingleton<MongoContext>();

            // Repositories
            services.AddScoped<IUserRepository, MongoUserRepository>();
            services.AddScoped<IItemRepository, MongoItemRepository>();
            services.AddScoped<IRentalRepository, MongoRentalRepository>();
            services.AddScoped<ILicenseRepository, MongoLicenseRepository>();

            return services;
        }
    }
}
