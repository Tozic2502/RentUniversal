using RentUniversal.Application.DTOs;
using RentUniversal.Domain.Entities;

namespace RentUniversal.Application.Interfaces;

/// <summary>
/// Service abstraction for rental-related business logic.
/// Handles starting/ending rentals, retrieving rental history, and pricing rules.
/// </summary>
public interface IRentalService
{
    /// <summary>
    /// Retrieves a single rental as a DTO by its identifier.
    /// Returns null if the rental does not exist.
    /// </summary>
    /// <param name="rentalId">The rental identifier.</param>
    /// <returns>The matching <see cref="RentalDTO"/> or null.</returns>
    Task<RentalDTO?> GetRentalAsync(string rentalId);

    /// <summary>
    /// Starts a new rental for the given user and item.
    /// creates a rental record, sets <c>StartDate</c>, and stores the initial item condition.
    /// </summary>
    /// <param name="userId">Identifier of the user starting the rental.</param>
    /// <param name="itemId">Identifier of the item being rented.</param>
    /// <param name="startCondition">Recorded condition of the item at pickup/start.</param>
    /// <returns>The created rental as a <see cref="RentalDTO"/>.</returns>
    Task<RentalDTO> StartRentalAsync(string userId, string itemId, string startCondition);

    /// <summary>
    /// Ends an existing rental.
    /// Typically, sets <c>EndDate</c>, stores the return condition.
    /// </summary>
    /// <param name="rentalId">Identifier of the rental to close.</param>
    /// <param name="returnCondition">Recorded condition of the item on return.</param>
    /// <returns>The updated rental as a <see cref="RentalDTO"/>.</returns>
    Task<RentalDTO> EndRentalAsync(string rentalId, string returnCondition);
    Task<bool> UpdateRentalAsync(RentalDTO rental);
    Task CreateAsync(Rental rental);

    /// <summary>
    /// Retrieves all rentals for a specific user as DTOs.
    /// </summary>
    /// <param name="userId">The user's identifier.</param>
    /// <returns>A collection of <see cref="RentalDTO"/> for the user.</returns>
    Task<IEnumerable<Rental>> GetByUserIdAsync(string userId);
    /// <summary>
    /// Calculates the rental price based on the provided rental data and pricing rules.
    /// The returned value may be used when ending a rental or for previews/estimates.
    /// </summary>
    /// <param name="rental">The rental used as input for the pricing calculation.</param>
    /// <returns>The calculated price.</returns>
    double CalculatePrice(Rental rental);
}