using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using RentUniversal.Application.Interfaces;
using RentUniversal.Infrastructure.Configuration;
using RentUniversal.Infrastructure.Data;
using RentUniversal.Infrastructure.Repositories;

namespace RentUniversal.Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Configures and registers the infrastructure services for the application.
        /// This includes setting up MongoDB settings, creating a MongoDB client, and registering
        /// the necessary repositories and context for dependency injection.
        /// </summary>
        /// <param name="services">The IServiceCollection to which the services will be added.</param>
        /// <param name="configuration">The application configuration containing MongoDB settings.</param>
        /// <returns>The updated IServiceCollection with the registered infrastructure services.</returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Load Mongo settings
            var mongoSettings = new MongoDbSettings();
            configuration.GetSection("MongoDb").Bind(mongoSettings);
            Console.WriteLine("MongoDB Connection: " + mongoSettings.ConnectionString);


            services.AddSingleton<IMongoDbSettings>(mongoSettings);

            // Configure Mongo client with authentication + server version
            var settings = MongoClientSettings.FromConnectionString(mongoSettings.ConnectionString);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);

            var client = new MongoClient(settings);
            services.AddSingleton<IMongoClient>(client);

            // Register MongoContext using the DI client
            services.AddSingleton<MongoContext>();

            // Register repositories
            services.AddScoped<IUserRepository, MongoUserRepository>();
            services.AddScoped<IItemRepository, MongoItemRepository>();
            services.AddScoped<IRentalRepository, MongoRentalRepository>();
            services.AddScoped<ILicenseRepository, MongoLicenseRepository>();

            return services;
        }
    }
}