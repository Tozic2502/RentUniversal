using MongoDB.Driver;
using RentUniversal.Application.Interfaces;

namespace RentUniversal.Infrastructure.Data
{
    public class MongoContext
    {
        private readonly IMongoClient _client;
        private readonly IMongoDbSettings _settings;

        public MongoContext(IMongoClient client, IMongoDbSettings settings)
        {
            _client = client;
            _settings = settings;
        }

        public IMongoDatabase Database => _client.GetDatabase(_settings.Database);
    }
}
