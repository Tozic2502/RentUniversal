using MongoDB.Driver;
using RentUniversal.Application.Interfaces;

namespace RentUniversal.Infrastructure.Data
{
    /// <summary>
    /// Represents the MongoDB context for interacting with the database.
    /// This class provides access to the MongoDB database instance using the provided client and settings.
    /// </summary>
    public class MongoContext
    {
        // The MongoDB client used to establish a connection to the database.
        private readonly IMongoClient _client;

        // The settings containing configuration details for the MongoDB connection (e.g., database name, connection string).
        private readonly IMongoDbSettings _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoContext"/> class.
        /// </summary>
        /// <param name="client">The MongoDB client used to connect to the database.</param>
        /// <param name="settings">The settings containing the database configuration.</param>
        public MongoContext(IMongoClient client, IMongoDbSettings settings)
        {
            _client = client;
            _settings = settings;
        }

        /// <summary>
        /// Gets the MongoDB database instance based on the configured database name.
        /// </summary>
        public IMongoDatabase Database => _client.GetDatabase(_settings.Database);
    }
}