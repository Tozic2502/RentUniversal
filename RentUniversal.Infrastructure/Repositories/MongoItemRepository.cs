using MongoDB.Driver;
using RentUniversal.Application.Interfaces;
using RentUniversal.Domain.Entities;
using RentUniversal.Infrastructure.Data;

namespace RentUniversal.Infrastructure.Repositories
{
    public class MongoItemRepository : IItemRepository
    {
        private readonly IMongoCollection<Item> _items;

        public MongoItemRepository(MongoContext context)
        {
            _items = context.GetCollection<Item>("Items");
        }

        public async Task CreateAsync(Item item) => await _items.InsertOneAsync(item);

        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            var res = await _items.FindAsync(_ => true);
            return await res.ToListAsync();
        }

        public async Task<Item?> GetByIdAsync(string id) =>
            await (await _items.FindAsync(i => i.Id == id)).FirstOrDefaultAsync();

        public async Task UpdateAsync(Item item) =>
            await _items.ReplaceOneAsync(i => i.Id == item.Id, item, new ReplaceOptions { IsUpsert = false });
    }
}
