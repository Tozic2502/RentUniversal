using RentUniversal.Application.DTOs;
using RentUniversal.Domain.Entities;

namespace RentUniversal.Application.Mapper;

public static class DTOMapper
{
    public static UserDTO ToDTO(User user) => new()
    {
        Id = user.Id,
        Name = user.Name,
        Email = user.Email,
        Role = user.Role.ToString(),
        IdentificationId = user.IdentificationId
    };

    public static ItemDTO ToDTO(Item item) => new ItemDTO
    {
        Id = item.Id,
        Name = item.Name,
        Category = item.Category,
        Condition = item.Condition,
        Value = item.Value,
        IsAvailable = item.IsAvailable,
        Deposit = item.Deposit,
        PricePerDay = item.PricePerDay
    };


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


    public static LicenseDTO ToDTO(License license) => new()
    {
        Id = license.Id,
        OwnerName = license.OwnerName,
        StartDate = license.StartDate,
        ExpiryDate = license.ExpiryDate
    };

    public static IdentificationDTO ToDTO(Identification id) => new()
    {
        Id = id.Id,
        Type = id.Type,
        DocumentUrl = id.DocumentUrl,
        VerifiedDate = id.VerifiedDate
    };
}