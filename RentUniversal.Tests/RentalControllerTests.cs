using Xunit;
using Moq;
using RentUniversal.api.Controllers;
using RentUniversal.Application.Interfaces;
using RentUniversal.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

public class RentalsControllerTests
{
    private readonly Mock<IRentalService> _rentalServiceMock;
    private readonly Mock<IItemService> _itemServiceMock;
    private readonly RentalsController _controller;

    public RentalsControllerTests()
    {
        _rentalServiceMock = new Mock<IRentalService>();
        _itemServiceMock = new Mock<IItemService>();
        _controller = new RentalsController(
            _rentalServiceMock.Object,
            _itemServiceMock.Object
        );
    }

    [Fact]
    public async Task ReturnRental_RentalNotFound_ReturnsNotFound()
    {
        // Arrange
        _rentalServiceMock.Setup(s => s.GetRentalAsync("1"))
                          .ReturnsAsync((RentalDTO?)null);

        // Act
        var result = await _controller.ReturnRental("1");

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task ReturnRental_ValidRental_ReturnsOk()
    {
        // Arrange
        var rental = new RentalDTO
        {
            Id = "1",
            ItemId = "item1",
            StartDate = DateTime.UtcNow.AddDays(-2),
            PricePerDay = 100
        };

        _rentalServiceMock.Setup(s => s.GetRentalAsync("1"))
                          .ReturnsAsync(rental);

        _rentalServiceMock.Setup(s => s.UpdateRentalAsync(It.IsAny<RentalDTO>()))
                          .ReturnsAsync(true);

        // Act
        var result = await _controller.ReturnRental("1");

        // Assert
        Assert.IsType<OkResult>(result);
    }
}
