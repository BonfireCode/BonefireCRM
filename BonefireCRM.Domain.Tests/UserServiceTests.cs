using AutoFixture;
using BonefireCRM.Domain.DTOs.User;
using BonefireCRM.Domain.Entities;
using BonefireCRM.Domain.Infrastructure.Persistance;
using BonefireCRM.Domain.Infrastructure.Security;
using BonefireCRM.Domain.Services;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace BonefireCRM.Domain.Tests
{
    public class UserServiceTests
    {
        private readonly IUserRepository _userRepository;
        private readonly IAppUserManager _appUserManager;
        private readonly IFixture _fixture;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _appUserManager = Substitute.For<IAppUserManager>();
            _fixture = new Fixture();

            var securityService = new SecurityService(
                _appUserManager,
                Substitute.For<IAppSignInManager>(),
                _userRepository,
                null);
            _userService = new UserService(_userRepository, securityService);
        }

        [Fact]
        public async Task GetUserAsync_NoUserFound_ReturnNoneAsync()
        {
            // Arange
            var id = _fixture.Create<Guid>();

            _userRepository.GetAsync(Arg.Any<Guid>(), CancellationToken.None)
                .ReturnsNullForAnyArgs();

            // Act
            var result = await _userService.GetUserAsync(id, CancellationToken.None);

            // Assert
            await _userRepository.Received(1).GetAsync(Arg.Any<Guid>(), CancellationToken.None);

            result.IsNone.Should().BeTrue();
        }

        [Fact]
        public async Task GetUserAsync_UserFound_ReturnUserAsync()
        {
            // Arange
            var id = _fixture.Create<Guid>();
            var user = _fixture.Build<User>()
                .Without(u => u.Activities)
                .With(u => u.Id, id)
                .Create();
            _userRepository.GetAsync(id, CancellationToken.None)
                .Returns(user);

            var expectedUser = _fixture.Build<GetUserDTO>()
                .With(dto => dto.Id, id)
                .With(dto => dto.FirstName, user.FirstName)
                .With(dto => dto.LastName, user.LastName)
                .Create();

            // Act
            var result = await _userService.GetUserAsync(id, CancellationToken.None);

            // Assert
            await _userRepository.Received(1).GetAsync(id, CancellationToken.None);

            result.IsSome.Should().BeTrue();
            result.IfSome(userDto =>
            {
                userDto.Id.Should().Be(expectedUser.Id);
                userDto.FirstName.Should().Be(expectedUser.FirstName);
                userDto.LastName.Should().Be(expectedUser.LastName);
            });
        }

        [Fact]
        public async Task DeleteUserAsync_SecurityDeleteFails_ReturnFailAsync()
        {
            // Arrange
            var id = _fixture.Create<Guid>();
            var registerId = _fixture.Create<Guid>();
            
            _appUserManager.DeleteUserAsync(registerId)
                .Returns(false);

            // Act
            var result = await _userService.DeleteUserAsync(id, registerId, CancellationToken.None);

            // Assert
            await _appUserManager.Received(1).DeleteUserAsync(registerId);
            await _userRepository.DidNotReceive().DeleteAsync(Arg.Any<User>(), CancellationToken.None);

            result.IsFail.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteUserAsync_SecurityDeleteSucceeds_RepositoryDeletesTrue_ReturnSuccTrueAsync()
        {
            // Arrange
            var id = _fixture.Create<Guid>();
            var registerId = _fixture.Create<Guid>();

            _appUserManager.DeleteUserAsync(registerId)
                .Returns(true);

            _userRepository.DeleteAsync(Arg.Any<User>(), CancellationToken.None)
                .Returns(true);

            // Act
            var result = await _userService.DeleteUserAsync(id, registerId, CancellationToken.None);

            // Assert
            await _appUserManager.Received(1).DeleteUserAsync(registerId);
            await _userRepository.Received(1).DeleteAsync(Arg.Is<User>(u => u.Id == id && u.RegisterId == registerId), CancellationToken.None);

            result.IsSucc.Should().BeTrue();
            result.IfSucc(deleted => deleted.Should().BeTrue());
        }

        [Fact]
        public async Task DeleteUserAsync_SecurityDeleteSucceeds_RepositoryDeletesFalse_ReturnFailAsync()
        {
            // Arrange
            var id = _fixture.Create<Guid>();
            var registerId = _fixture.Create<Guid>();

            _appUserManager.DeleteUserAsync(registerId)
                .Returns(true);

            _userRepository.DeleteAsync(Arg.Any<User>(), CancellationToken.None)
                .Returns(false);

            // Act
            var result = await _userService.DeleteUserAsync(id, registerId, CancellationToken.None);

            // Assert
            await _appUserManager.Received(1).DeleteUserAsync(registerId);
            await _userRepository.Received(1).DeleteAsync(Arg.Is<User>(u => u.Id == id && u.RegisterId == registerId), CancellationToken.None);

            result.IsFail.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateUserAsync_UpdateSucceeds_ReturnUpdatedDtoAsync()
        {
            // Arrange
            var id = _fixture.Create<Guid>();
            var updatedUser = _fixture.Build<User>()
                .Without(u => u.Activities)
                .With(u => u.Id, id)
                .Create();

            _userRepository.UpdateAsync(Arg.Any<User>(), CancellationToken.None)
                .Returns(updatedUser);

            var expectedUpdatedDto = _fixture.Build<UpdatedUserDTO>()
                .With(dto => dto.Id, id)
                .With(dto => dto.FirstName, updatedUser.FirstName)
                .With(dto => dto.LastName, updatedUser.LastName)
                .Create();

            var updateDto = _fixture.Create<UpdateUserDTO>();

            // Act
            var result = await _userService.UpdateUserAsync(updateDto, CancellationToken.None);

            // Assert
            await _userRepository.Received(1).UpdateAsync(Arg.Any<User>(), CancellationToken.None);

            result.IsSucc.Should().BeTrue();
            result.IfSucc(dto =>
            {
                dto.Id.Should().Be(expectedUpdatedDto.Id);
                dto.FirstName.Should().Be(expectedUpdatedDto.FirstName);
                dto.LastName.Should().Be(expectedUpdatedDto.LastName);
            });
        }

        [Fact]
        public async Task UpdateUserAsync_UpdateFails_ReturnFailAsync()
        {
            // Arrange
            _userRepository.UpdateAsync(Arg.Any<User>(), CancellationToken.None)
                .ReturnsNullForAnyArgs();

            var updateDto = _fixture.Create<UpdateUserDTO>();

            // Act
            var result = await _userService.UpdateUserAsync(updateDto, CancellationToken.None);

            // Assert
            await _userRepository.Received(1).UpdateAsync(Arg.Any<User>(), CancellationToken.None);

            result.IsFail.Should().BeTrue();
        }

        [Fact]
        public async Task GetUserIdAsync_ReturnsUserIdAsync()
        {
            // Arrange
            var registerId = _fixture.Create<Guid>();
            var userId = _fixture.Create<Guid>();

            _userRepository.GetUserIdAsync(registerId, CancellationToken.None)
                .Returns(userId);

            // Act
            var result = await _userService.GetUserIdAsync(registerId, CancellationToken.None);

            // Assert
            await _userRepository.Received(1).GetUserIdAsync(registerId, CancellationToken.None);

            result.Should().Be(userId);
        }
    }
}
