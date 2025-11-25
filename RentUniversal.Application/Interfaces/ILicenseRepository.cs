using RentUniversal.Domain.Entities;

namespace RentUniversal.Application.Interfaces
{
    public interface ILicenseRepository
    {
        Task<License?> GetAsync();
        Task UpsertAsync(License license);
    }
}

