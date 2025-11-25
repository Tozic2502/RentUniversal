using MongoDB.Driver;
using RentUniversal.Application.Interfaces;

namespace RentUniversal.Infrastructure.Data
{
    public class MongoContext
    {
        private readonly IMongoDatabase _database;


        public MongoContext(IMongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            _database = client.GetDatabase(settings.Database);
        }

        public IMongoCollection<T> GetCollection<T>(string name) =>
            _database.GetCollection<T>(name);

    }
}
