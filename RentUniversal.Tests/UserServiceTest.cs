using Xunit;
using Moq;
using RentUniversal.Application.Services;
using RentUniversal.Application.Interfaces;
using RentUniversal.Domain.Entities;
using System;
using System.Threading.Tasks;

public class UserServiceTests
{
	private readonly Mock<IUserRepository> _repoMock;
	private readonly UserService _service;

	public UserServiceTests()
	{
		_repoMock = new Mock<IUserRepository>();
		_service = new UserService(_repoMock.Object);
	}

	[Fact]
	public async Task RegisterAsync_ValidUser_CreatesUser()
	{
		// Arrange
		var user = new User
		{
			Name = "Test User",
			Email = "test@test.com",
			IdentificationId = 1234567890
		};

		_repoMock.Setup(r => r.GetByEmailAsync(user.Email))
				 .ReturnsAsync((User?)null);

		_repoMock.Setup(r => r.GetByIdentificationIdAsync(user.IdentificationId))
				 .ReturnsAsync((User?)null);

		// Act
		var result = await _service.RegisterAsync(user, "password123");

		// Assert
		Assert.NotNull(result);
		Assert.Equal(user.Email, result.Email);

		_repoMock.Verify(r => r.CreateAsync(It.IsAny<User>()), Times.Once);
	}

	[Fact]
	public async Task RegisterAsync_DuplicateEmail_ThrowsException()
	{
		// Arrange
		var user = new User
		{
			Name = "Test",
			Email = "test@test.com"
		};

		_repoMock.Setup(r => r.GetByEmailAsync(user.Email))
				 .ReturnsAsync(new User());

		// Act & Assert
		await Assert.ThrowsAsync<Exception>(() =>
			_service.RegisterAsync(user, "password123")
		);
	}
}
