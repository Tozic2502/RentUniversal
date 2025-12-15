using MongoDB.Driver;
using RentUniversal.Application.Interfaces;
using RentUniversal.Domain.Entities;
using RentUniversal.Infrastructure.Data;

namespace RentUniversal.Infrastructure.Repositories
{
    /// <summary>
    /// A repository implementation for managing License entities in a MongoDB database.
    /// This class provides methods to retrieve and upsert (update or insert) License documents.
    /// </summary>
    public class MongoLicenseRepository : ILicenseRepository
    {
        private readonly IMongoCollection<License> _licenses;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoLicenseRepository"/> class.
        /// Sets up the MongoDB collection for License entities.
        /// </summary>
        /// <param name="context">The MongoDB context used to access the database.</param>
        public MongoLicenseRepository(MongoContext context)
        {
            _licenses = context.Database.GetCollection<License>("Licenses");
        }

        /// <summary>
        /// Retrieves the first License document from the MongoDB collection.
        /// If no document exists, returns null.
        /// </summary>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// The task result contains the first <see cref="License"/> document or null if none exist.
        /// </returns>
        public async Task<License?> GetAsync() =>
            await (await _licenses.FindAsync(_ => true)).FirstOrDefaultAsync();

        /// <summary>
        /// Inserts a new License document or updates an existing one in the MongoDB collection.
        /// If a document already exists, it is replaced; otherwise, a new document is created.
        /// </summary>
        /// <param name="license">The <see cref="License"/> entity to upsert.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task UpsertAsync(License license) =>
            await _licenses.ReplaceOneAsync(_ => true, license, new ReplaceOptions { IsUpsert = true });
    }
}