using AutoFixture;
using BonefireCRM.Domain.DTOs.User;
using BonefireCRM.Domain.Entities;
using BonefireCRM.Domain.Infrastructure.Persistance;
using BonefireCRM.Domain.Services;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace BonefireCRM.Domain.Tests
{
    public class UserServiceTests
    {
        private readonly IUserRepository _userRepository;
        private readonly SecurityService _securityService;
        private readonly IFixture _fixture;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _fixture = new Fixture();

            _securityService = new SecurityService(null, null, null, null);
            _userService = new UserService(_userRepository, _securityService);
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
    }
}
