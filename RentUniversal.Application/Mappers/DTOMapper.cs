using RentUniversal.Application.DTOs;
using RentUniversal.Domain.Entities;

namespace RentUniversal.Application.Mapper;

/// <summary>
/// Central mapper for converting domain entities to DTOs.
/// This keeps controllers/services from repeating the same mapping logic everywhere.
/// </summary>
public static class DTOMapper
{
    /// <summary>
    /// Maps a domain <see cref="User"/> entity to a <see cref="UserDTO"/>.
    /// </summary>
    /// <param name="user">The domain user entity.</param>
    /// <returns>A DTO representation of the user.</returns>
    public static UserDTO ToDTO(User user) => new()
    {
        Id = user.Id,
        Name = user.Name,
        Email = user.Email,
        Role = user.Role,
        IdentificationId = user.IdentificationId,
        RegisteredDate = user.RegisteredDate,
        LastLogin = user.LastLogin
    };

    /// <summary>
    /// Maps a domain <see cref="Item"/> entity to an <see cref="ItemDTO"/>.
    /// </summary>
    /// <param name="item">The domain item entity.</param>
    /// <returns>A DTO representation of the item.</returns>
    public static ItemDTO ToDTO(Item item) => new ItemDTO
    {
        Id = item.Id,
        Name = item.Name,
        Category = item.Category,
        Condition = item.Condition,
        Value = item.Value,
        IsAvailable = item.IsAvailable,
        Deposit = item.Deposit,
        PricePerDay = item.PricePerDay,
        ImageUrls = item.ImageUrls
    };

    /// <summary>
    /// Maps a domain <see cref="Rental"/> entity to a <see cref="RentalDTO"/>.
    /// </summary>
    /// <param name="rental">The domain rental entity.</param>
    /// <returns>A DTO representation of the rental.</returns>
    public static RentalDTO ToDTO(Rental rental, Item? item = null) => new()
    {
        Id = rental.Id,
        UserId = rental.UserId,
        ItemId = rental.ItemId,
        StartDate = rental.RentalDate,
        EndDate = rental.ReturnDate,
        StartCondition = rental.StartCondition,
        ReturnCondition = rental.ReturnCondition,
        Price = rental.Price,
        PricePerDay = rental.PricePerDay,
        TotalPrice = rental.TotalPrice,

        Item = item != null ? DTOMapper.ToDTO(item) : null
    };


    /// <summary>
    /// Maps a domain <see cref="License"/> entity to a <see cref="LicenseDTO"/>.
    /// </summary>
    /// <param name="license">The domain license entity.</param>
    /// <returns>A DTO representation of the license.</returns>
    public static LicenseDTO ToDTO(License license) => new()
    {
        Id = license.Id,
        OwnerName = license.OwnerName,
        StartDate = license.StartDate,
        ExpiryDate = license.ExpiryDate
    };

    /// <summary>
    /// Maps a domain <see cref="Identification"/> entity to an <see cref="IdentificationDTO"/>.
    /// </summary>
    /// <param name="id">The domain identification entity.</param>
    /// <returns>A DTO representation of the identification record.</returns>
    public static IdentificationDTO ToDTO(Identification id) => new()
    {
        Id = id.Id,
        Type = id.Type,
        DocumentUrl = id.DocumentUrl,
        VerifiedDate = id.VerifiedDate
    };
}