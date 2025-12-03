using MongoDB.Driver;
using RentUniversal.Application.Interfaces;
using RentUniversal.Domain.Entities;
using RentUniversal.Infrastructure.Data;

namespace RentUniversal.Infrastructure.Repositories
{
    public class MongoUserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public MongoUserRepository(MongoContext context)
        {
            _users = context.Database.GetCollection<User>("Users");
        }

        public async Task CreateAsync(User user) =>
            await _users.InsertOneAsync(user);

        public async Task<IEnumerable<User>> GetAllAsync() =>
            await (await _users.FindAsync(_ => true)).ToListAsync();

        public async Task<User?> GetByEmailAsync(string email)
        {
            var result = await _users.FindAsync(u => u.Email == email);
            return await result.FirstOrDefaultAsync();
        }

        public async Task<User?> GetByIdAsync(string id) =>
            await (await _users.FindAsync(u => u.Id == id)).FirstOrDefaultAsync();

        public async Task UpdateAsync(User user)
        {
            await _users.ReplaceOneAsync(u => u.Id == user.Id, user);
        }



    }
}
    