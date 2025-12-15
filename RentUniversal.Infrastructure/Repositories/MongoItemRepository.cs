using MongoDB.Driver;
using RentUniversal.Application.Interfaces;
using RentUniversal.Domain.Entities;
using RentUniversal.Infrastructure.Data;

namespace RentUniversal.Infrastructure.Repositories
{
    /// <summary>
    /// A repository implementation for managing Item entities in a MongoDB database.
    /// This class provides CRUD (Create, Read, Update, Delete) operations for the Item collection.
    /// </summary>
    public class MongoItemRepository : IItemRepository
    {
        private readonly IMongoCollection<Item> _items;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoItemRepository"/> class.
        /// Sets up the MongoDB collection for Item entities using the provided MongoContext.
        /// </summary>
        /// <param name="context">The MongoDB context used to access the database.</param>
        public MongoItemRepository(MongoContext context)
        {
            _items = context.Database.GetCollection<Item>("Items");
        }

        /// <summary>
        /// Asynchronously creates a new Item in the database.
        /// </summary>
        /// <param name="item">The Item entity to be added to the database.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task CreateAsync(Item item) => await _items.InsertOneAsync(item);

        /// <summary>
        /// Asynchronously retrieves all Item entities from the database.
        /// </summary>
        /// <returns>A Task representing the asynchronous operation, containing a collection of all Items.</returns>
        public async Task<IEnumerable<Item>> GetAllAsync()
        {
            var res = await _items.FindAsync(_ => true);
            return await res.ToListAsync();
        }

        /// <summary>
        /// Asynchronously retrieves a single Item entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Item to retrieve.</param>
        /// <returns>
        /// A Task representing the asynchronous operation, containing the Item if found, or null if not found.
        /// </returns>
        public async Task<Item?> GetByIdAsync(string id) =>
            await (await _items.FindAsync(i => i.Id == id)).FirstOrDefaultAsync();

        /// <summary>
        /// Asynchronously updates an existing Item in the database.
        /// </summary>
        /// <param name="item">The Item entity with updated data.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task UpdateAsync(Item item) =>
            await _items.ReplaceOneAsync(i => i.Id == item.Id, item, new ReplaceOptions { IsUpsert = false });

        /// <summary>
        /// Asynchronously deletes an Item from the database by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Item to delete.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task DeleteAsync(string id)
        {
            await _items.DeleteOneAsync(i => i.Id == id);
        }
    }
}