using MongoDB.Driver;
using RentUniversal.Application.Interfaces;
using RentUniversal.Domain.Entities;
using RentUniversal.Infrastructure.Data;

namespace RentUniversal.Infrastructure.Repositories;

public class MongoContactRepository : IContactRepository
{
    private readonly IMongoCollection<Contact> _collection;

    public MongoContactRepository(MongoContext context)
    {
        _collection = context.Database.GetCollection<Contact>("ContactMessages");
    }

    public async Task CreateAsync(Contact contact)
    {
        await _collection.InsertOneAsync(contact);
    }

    public async Task<List<Contact>> GetAllAsync()
    {
        return await _collection
            .Find(_ => true)
            .SortByDescending(c => c.CreatedAt)
            .ToListAsync();
    }

    public async Task DeleteAsync(string id)
    {
        await _collection.DeleteOneAsync(c => c.Id == id);
    }
}