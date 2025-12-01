using MongoDB.Driver;
using RentUniversal.Application.Interfaces;
using RentUniversal.Domain.Entities;
using RentUniversal.Infrastructure.Data;

namespace RentUniversal.Infrastructure.Repositories
{
    public class MongoRentalRepository : IRentalRepository
    {
        private readonly IMongoCollection<Rental> _rentals;

        public MongoRentalRepository(MongoContext context)
        {
            _rentals = context.Database.GetCollection<Rental>("Rentals");
        }

        public async Task CreateAsync(Rental rental) => await _rentals.InsertOneAsync(rental);

        public async Task<Rental?> GetByIdAsync(string id) =>
            await (await _rentals.FindAsync(r => r.Id == id)).FirstOrDefaultAsync();

        public async Task<IEnumerable<Rental>> GetByUserIdAsync(string userId)
        {
            var res = await _rentals.FindAsync(r => r.UserId == userId);
            return await res.ToListAsync();
        }

        public async Task UpdateAsync(Rental rental) =>
            await _rentals.ReplaceOneAsync(r => r.Id == rental.Id, rental, new ReplaceOptions { IsUpsert = false });
    }
}
