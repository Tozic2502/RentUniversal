using MongoDB.Driver;
using RentUniversal.Application.Interfaces;
using RentUniversal.Domain.Entities;
using RentUniversal.Infrastructure.Data;

namespace RentUniversal.Infrastructure.Repositories
{
    public class MongoLicenseRepository : ILicenseRepository
    {
        private readonly IMongoCollection<License> _licenses;

        public MongoLicenseRepository(MongoContext context)
        {
            _licenses = context.Database.GetCollection<License>("Licenses");
        }

        public async Task<License?> GetAsync() =>
            await (await _licenses.FindAsync(_ => true)).FirstOrDefaultAsync();

        public async Task UpsertAsync(License license) =>
            await _licenses.ReplaceOneAsync(_ => true, license, new ReplaceOptions { IsUpsert = true });
    }
}