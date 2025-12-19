using MongoDB.Driver;
using RentUniversal.Application.Interfaces;
using RentUniversal.Domain.Entities;
using RentUniversal.Infrastructure.Data;

namespace RentUniversal.Infrastructure.Repositories
{
    /// <summary>
    /// A repository implementation for managing Rental entities in a MongoDB database.
    /// This class provides methods to perform CRUD operations on the "Rentals" collection.
    /// </summary>
    public class MongoRentalRepository : IRentalRepository
    {
        private readonly IMongoCollection<Rental> _rentals;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoRentalRepository"/> class.
        /// </summary>
        /// <param name="context">The MongoDB context used to access the database.</param>
        public MongoRentalRepository(MongoContext context)
        {
            // Access the "Rentals" collection from the MongoDB database.
            _rentals = context.Database.GetCollection<Rental>("Rentals");
        }

        /// <summary>
        /// Adds a new Rental entity to the "Rentals" collection.
        /// </summary>
        /// <param name="rental">The Rental entity to be added.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task CreateAsync(Rental rental) => await _rentals.InsertOneAsync(rental);

        /// <summary>
        /// Retrieves a Rental entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the Rental entity.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains the Rental entity
        /// if found, or null if no entity matches the provided identifier.
        /// </returns>
        public async Task<Rental?> GetByIdAsync(string id) =>
            await (await _rentals.FindAsync(r => r.Id == id)).FirstOrDefaultAsync();

        /// <summary>
        /// Retrieves all Rental entities associated with a specific user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>
        /// A task representing the asynchronous operation. The task result contains a collection of
        /// Rental entities associated with the specified user.
        /// </returns>
        public async Task<IEnumerable<Rental>> GetByUserIdAsync(string userId)
        {
            var res = await _rentals.FindAsync(r => r.UserId == userId);
            return await res.ToListAsync();
        }

        /// <summary>
        /// Updates an existing Rental entity in the "Rentals" collection.
        /// </summary>
        /// <param name="rental">The Rental entity with updated data.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task UpdateAsync(Rental rental) =>
            await _rentals.ReplaceOneAsync(r => r.Id == rental.Id, rental, new ReplaceOptions { IsUpsert = false });
    }
}