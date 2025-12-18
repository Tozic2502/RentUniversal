using MongoDB.Driver;
using RentUniversal.Application.Interfaces;
using RentUniversal.Domain.Entities;
using RentUniversal.Infrastructure.Data;

namespace RentUniversal.Infrastructure.Repositories;

/// <summary>
/// MongoDB implementation of the <see cref="IContactRepository"/> interface.
/// Handles persistence and retrieval of contact messages stored in MongoDB.
/// </summary>
public class MongoContactRepository : IContactRepository
{
    private readonly IMongoCollection<Contact> _collection;

    /// <summary>
    /// Initializes a new instance of the <see cref="MongoContactRepository"/> class.
    /// Sets up the MongoDB collection used to store contact messages.
    /// </summary>
    /// <param name="context">
    /// The MongoDB context providing access to the database.
    /// </param>
    public MongoContactRepository(MongoContext context)
    {
        _collection = context.Database.GetCollection<Contact>("ContactMessages");
    }

    /// <summary>
    /// Creates and stores a new contact message in the database.
    /// </summary>
    /// <param name="contact">
    /// The contact entity containing name, email, message and timestamp.
    /// </param>
    public async Task CreateAsync(Contact contact)
    {
        await _collection.InsertOneAsync(contact);
    }

    /// <summary>
    /// Retrieves all contact messages from the database.
    /// Messages are returned sorted by creation date in descending order
    /// so the newest messages appear first.
    /// </summary>
    /// <returns>
    /// A list of all stored contact messages.
    /// </returns>
    public async Task<List<Contact>> GetAllAsync()
    {
        return await _collection
            .Find(_ => true)
            .SortByDescending(c => c.CreatedAt)
            .ToListAsync();
    }

    /// <summary>
    /// Deletes a contact message from the database based on its identifier.
    /// </summary>
    /// <param name="id">
    /// The unique identifier of the contact message to delete.
    /// </param>
    public async Task DeleteAsync(string id)
    {
        await _collection.DeleteOneAsync(c => c.Id == id);
    }
}
