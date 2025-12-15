using MongoDB.Driver;
using RentUniversal.Application.Interfaces;
using RentUniversal.Domain.Entities;
using RentUniversal.Infrastructure.Data;

namespace RentUniversal.Infrastructure.Repositories
{
    /// <summary>
    /// A repository implementation for managing User entities in a MongoDB database.
    /// This class provides methods to perform CRUD (Create, Read, Update) operations
    /// on the "Users" collection within the MongoDB database.
    /// </summary>
    public class MongoUserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoUserRepository"/> class.
        /// </summary>
        /// <param name="context">The MongoDB context used to access the database.</param>
        public MongoUserRepository(MongoContext context)
        {
            _users = context.Database.GetCollection<User>("Users");
        }

        /// <summary>
        /// Adds a new User entity to the "Users" collection.
        /// </summary>
        /// <param name="user">The User entity to be added.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task CreateAsync(User user) =>
            await _users.InsertOneAsync(user);

        /// <summary>
        /// Retrieves all User entities from the "Users" collection.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, containing a list of all Users.</returns>
        public async Task<IEnumerable<User>> GetAllAsync() =>
            await (await _users.FindAsync(_ => true)).ToListAsync();

        /// <summary>
        /// Retrieves a User entity by its email address.
        /// </summary>
        /// <param name="email">The email address of the User to retrieve.</param>
        /// <returns>A task representing the asynchronous operation, containing the User entity if found; otherwise, null.</returns>
        public async Task<User?> GetByEmailAsync(string email)
        {
            var result = await _users.FindAsync(u => u.Email == email);
            return await result.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Retrieves a User entity by its identification ID.
        /// </summary>
        /// <param name="identificationId">The identification ID of the User to retrieve.</param>
        /// <returns>A task representing the asynchronous operation, containing the User entity if found; otherwise, null.</returns>
        public async Task<User?> GetByIdentificationIdAsync(int identificationId)
        {
            var result = await _users.FindAsync(u => u.IdentificationId == identificationId);
            return await result.FirstOrDefaultAsync();
        }

        /// <summary>
        /// Retrieves a User entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the User to retrieve.</param>
        /// <returns>A task representing the asynchronous operation, containing the User entity if found; otherwise, null.</returns>
        public async Task<User?> GetByIdAsync(string id) =>
            await (await _users.FindAsync(u => u.Id == id)).FirstOrDefaultAsync();

        /// <summary>
        /// Updates an existing User entity in the "Users" collection.
        /// </summary>
        /// <param name="user">The User entity with updated data.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task UpdateAsync(User user)
        {
            await _users.ReplaceOneAsync(u => u.Id == user.Id, user);
        }
    }
}