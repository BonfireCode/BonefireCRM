using AutoFixture;
using BonefireCRM.Domain.DTOs.Security;
using BonefireCRM.Domain.Entities;
using BonefireCRM.Domain.Exceptions;
using BonefireCRM.Domain.Infrastructure.Persistance;
using BonefireCRM.Domain.Infrastructure.Security;
using BonefireCRM.Domain.Services;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System.Security.Claims;

namespace BonefireCRM.Domain.Tests
{
    public class SecurityServiceTests
    {
        private readonly IAppUserManager _appUserManager;
        private readonly IAppSignInManager _appSignInManager;
        private readonly IUserRepository _userRepository;
        private readonly IFixture _fixture;
        private readonly SecurityService _securityService;

        public SecurityServiceTests()
        {
            _appUserManager = Substitute.For<IAppUserManager>();
            _appSignInManager = Substitute.For<IAppSignInManager>();
            _userRepository = Substitute.For<IUserRepository>();
            _fixture = new Fixture();

            var seedUserDataService = new SeedUserDataService(Substitute.For<IBaseRepository<DealParticipantRole>>());
            _securityService = new SecurityService(_appUserManager, _appSignInManager, _userRepository, seedUserDataService);
        }

        [Fact]
        public async Task RegisterUser_AppUserManagerReturnsValidationError_ReturnFailAsync()
        {
            //Arrange
            var registerDto = _fixture.Create<RegisterDTO>();
            var registerResultDTO = new RegisterResultDTO
            {
                UserId = Guid.Empty,
                ValidationError = new KeyValuePair<string, string>("error", "errorMessage")
            };
            _appUserManager.CreateAsync(Arg.Any<RegisterDTO>())
                .Returns(registerResultDTO);

            //Act
            var result = await _securityService.RegisterUser(registerDto, CancellationToken.None);

            //Assert
            await _appUserManager.Received(1).CreateAsync(Arg.Any<RegisterDTO>());

            result.IsFail.Should().BeTrue();
            result.IfFail(e => e.ToException().Should().BeOfType<RegisterUserException>());
        }

        [Fact]
        public async Task RegisterUser_UserRepositoryAddFails_ReturnFailAsync()
        {
            //Arrange
            var registerDto = _fixture.Create<RegisterDTO>();
            var registerResultDTO = new RegisterResultDTO
            {
                UserId = Guid.NewGuid(),
                ValidationError = new KeyValuePair<string, string>(string.Empty, string.Empty)
            };
            _appUserManager.CreateAsync(Arg.Any<RegisterDTO>())
                .Returns(registerResultDTO);

            _userRepository.AddAsync(Arg.Any<User>(), CancellationToken.None)
                .ReturnsNullForAnyArgs();

            //Act
            var result = await _securityService.RegisterUser(registerDto, CancellationToken.None);

            //Assert
            await _appUserManager.Received(1).CreateAsync(Arg.Any<RegisterDTO>());
            await _userRepository.Received(1).AddAsync(Arg.Any<User>(), CancellationToken.None);

            result.IsFail.Should().BeTrue();
            result.IfFail(e => e.ToException().Should().BeOfType<AddEntityException<User>>());
        }

        [Fact]
        public async Task RegisterUser_Succeeds_ReturnRegisterResultWithUserIdAsync()
        {
            //Arrange
            var registerDto = _fixture.Create<RegisterDTO>();
            var registerResult = new RegisterResultDTO
            {
                UserId = Guid.NewGuid(),
                ValidationError = new KeyValuePair<string, string>(string.Empty, string.Empty)
            };
            _appUserManager.CreateAsync(Arg.Any<RegisterDTO>())
                .Returns(registerResult);

            var createdUser = _fixture.Build<User>()
                .Without(u => u.Activities)
                .Create();
            _userRepository.AddAsync(Arg.Any<User>(), CancellationToken.None)
                .Returns(createdUser);

            //Act
            var result = await _securityService.RegisterUser(registerDto, CancellationToken.None);

            //Assert
            await _appUserManager.Received(1).CreateAsync(Arg.Any<RegisterDTO>());
            await _userRepository.Received(1).AddAsync(Arg.Any<User>(), CancellationToken.None);

            result.IsSucc.Should().BeTrue();
            result.IfSucc(r => r.UserId.Should().Be(createdUser.Id));
        }

        [Fact]
        public async Task LoginUser_ReturnsLoginResultAsync()
        {
            //Arrange
            var loginDto = _fixture.Create<LoginDTO>();
            var loginResult = _fixture.Create<LoginResultDTO>();
            _appSignInManager.PasswordSignInAsync(loginDto)
                .Returns(loginResult);

            //Act
            var result = await _securityService.LoginUser(loginDto, CancellationToken.None);

            //Assert
            await _appSignInManager.Received(1).PasswordSignInAsync(loginDto);

            result.Should().Be(loginResult);
        }

        [Fact]
        public async Task RefreshUserToken_ReturnsRefreshResultAsync()
        {
            //Arrange
            var refreshDto = _fixture.Create<RefreshDTO>();
            var refreshResult = _fixture.Create<RefreshResultDTO>();
            _appSignInManager.RefreshUserToken(refreshDto)
                .Returns(refreshResult);

            //Act
            var result = await _securityService.RefreshUserToken(refreshDto, CancellationToken.None);

            //Assert
            await _appSignInManager.Received(1).RefreshUserToken(refreshDto);

            result.Should().Be(refreshResult);
        }

        [Fact]
        public async Task ConfirmUserEmail_Fails_ReturnFailAsync()
        {
            //Arrange
            var confirmDto = _fixture.Create<ConfirmEmailDTO>();
            _appUserManager.ConfirmEmailAsync(confirmDto)
                .Returns(false);

            //Act
            var result = await _securityService.ConfirmUserEmail(confirmDto, CancellationToken.None);

            //Assert
            await _appUserManager.Received(1).ConfirmEmailAsync(confirmDto);

            result.IsFail.Should().BeTrue();
            result.IfFail(e => e.ToException().Should().BeOfType<UnauthorisedException>());
        }

        [Fact]
        public async Task ConfirmUserEmail_Succeeds_ReturnTrueAsync()
        {
            //Arrange
            var confirmDto = _fixture.Create<ConfirmEmailDTO>();
            _appUserManager.ConfirmEmailAsync(confirmDto)
                .Returns(true);

            //Act
            var result = await _securityService.ConfirmUserEmail(confirmDto, CancellationToken.None);

            //Assert
            await _appUserManager.Received(1).ConfirmEmailAsync(confirmDto);

            result.IsSucc.Should().BeTrue();
        }

        [Fact]
        public async Task ManageUserTwoFactor_AppUserManagerReturnsValidationError_ReturnFailAsync()
        {
            //Arrange
            var twoFactorDto = _fixture.Create<TwoFactorDTO>();
            var keyAndCodes = new KeyAndRecoveryCodesDTO
            {
                Key = string.Empty,
                RecoveryCodes = [],
                RecoveryCodesLeft = 0,
                IsTwoFactorEnabled = false,
                ValidationError = new KeyValuePair<string, string>("error", "errorMessage")
            };
            _appUserManager.GetKeyAndRecoveryCodes(twoFactorDto, Arg.Any<ClaimsPrincipal>())
                .Returns(keyAndCodes);

            //Act
            var result = await _securityService.ManageUserTwoFactor(twoFactorDto, new ClaimsPrincipal(), CancellationToken.None);

            //Assert
            await _appUserManager.Received(1).GetKeyAndRecoveryCodes(twoFactorDto, Arg.Any<ClaimsPrincipal>());

            result.IsFail.Should().BeTrue();
            result.IfFail(e => e.ToException().Should().BeOfType<TwoFactorException>());
        }

        [Fact]
        public async Task ManageUserTwoFactor_Succeeds_ReturnTwoFactorResultAsync()
        {
            //Arrange
            var twoFactorDto = _fixture.Create<TwoFactorDTO>();
            var keyAndCodes = new KeyAndRecoveryCodesDTO
            {
                Key = "key",
                RecoveryCodes = ["a"],
                RecoveryCodesLeft = 1,
                IsTwoFactorEnabled = true,
                ValidationError = new KeyValuePair<string, string>(string.Empty, string.Empty)
            };
            _appUserManager.GetKeyAndRecoveryCodes(twoFactorDto, Arg.Any<ClaimsPrincipal>())
                .Returns(keyAndCodes);
            _appSignInManager.RememberMachine(twoFactorDto, Arg.Any<ClaimsPrincipal>())
                .Returns(true);

            //Act
            var result = await _securityService.ManageUserTwoFactor(twoFactorDto, new ClaimsPrincipal(), CancellationToken.None);

            //Assert
            await _appUserManager.Received(1).GetKeyAndRecoveryCodes(twoFactorDto, Arg.Any<ClaimsPrincipal>());
            await _appSignInManager.Received(1).RememberMachine(twoFactorDto, Arg.Any<ClaimsPrincipal>());

            result.IsSucc.Should().BeTrue();
            result.IfSucc(r =>
            {
                r.SharedKey.Should().Be(keyAndCodes.Key);
                r.RecoveryCodesLeft.Should().Be(keyAndCodes.RecoveryCodesLeft);
            });
        }

        [Fact]
        public async Task ManageGetUserInfo_AppUserManagerReturnsValidationError_ReturnFailAsync()
        {
            //Arrange
            var claim = new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString());
            var claimsidentity = new ClaimsIdentity([claim]);
            var claims = new ClaimsPrincipal(claimsidentity);
            var getInfo = new GetInfoResultDTO
            {
                Email = "test@test.com",
                IsEmailConfirmed = false,
                ValidationError = new KeyValuePair<string, string>("error", "errorMessage")
            };
            _appUserManager.GetInfo(Arg.Any<ClaimsPrincipal>())
                .Returns(getInfo);

            //Act
            var result = await _securityService.ManageGetUserInfo(claims, CancellationToken.None);

            //Assert
            await _appUserManager.Received(1).GetInfo(Arg.Any<ClaimsPrincipal>());

            result.IsFail.Should().BeTrue();
            result.IfFail(e => e.ToException().Should().BeOfType<GetInfoException>());
        }

        [Fact]
        public async Task ManageGetUserInfo_Succeeds_ReturnGetInfoWithUserIdAsync()
        {
            //Arrange
            var registerId = Guid.NewGuid();
            var claim = new Claim(ClaimTypes.NameIdentifier, registerId.ToString());
            var claimsidentity = new ClaimsIdentity([claim]);
            var claims = new ClaimsPrincipal(claimsidentity);
            var getInfo = new GetInfoResultDTO
            {
                Email = "test@test.com",
                IsEmailConfirmed = true,
                ValidationError = new KeyValuePair<string, string>(string.Empty, string.Empty)
            };
            _appUserManager.GetInfo(Arg.Any<ClaimsPrincipal>()).Returns(getInfo);
            _userRepository.GetUserIdAsync(registerId, CancellationToken.None)
                .Returns(Guid.NewGuid());

            //Act
            var result = await _securityService.ManageGetUserInfo(claims, CancellationToken.None);

            //Assert
            await _appUserManager.Received(1).GetInfo(Arg.Any<ClaimsPrincipal>());
            await _userRepository.Received(1).GetUserIdAsync(registerId, CancellationToken.None);

            result.IsSucc.Should().BeTrue();
            result.IfSucc(r => r.Email.Should().Be(getInfo.Email));
        }

        [Fact]
        public async Task ManageCreateUserInfo_AppUserManagerReturnsValidationError_ReturnFailAsync()
        {
            //Arrange
            var createInfoDto = _fixture.Create<CreateInfoDTO>();
            var createInfoResult = new CreateInfoResultDTO
            {
                Email = "test@test.com",
                IsEmailConfirmed = false,
                ValidationError = new KeyValuePair<string, string>("error", "errorMessage")
            };
            _appUserManager.CreateInfo(createInfoDto, Arg.Any<ClaimsPrincipal>())
                .Returns(createInfoResult);

            //Act
            var result = await _securityService.ManageCreateUserInfo(createInfoDto, new ClaimsPrincipal(), CancellationToken.None);

            //Assert
            await _appUserManager.Received(1).CreateInfo(createInfoDto, Arg.Any<ClaimsPrincipal>());

            result.IsFail.Should().BeTrue();
            result.IfFail(e => e.ToException().Should().BeOfType<CreateInfoException>());
        }

        [Fact]
        public async Task ManageCreateUserInfo_Succeeds_ReturnCreateInfoResultAsync()
        {
            //Arrange
            var createInfoDto = _fixture.Create<CreateInfoDTO>();
            var createInfoResult = new CreateInfoResultDTO
            {
                Email = "test@test.com",
                IsEmailConfirmed = true,
                ValidationError = new KeyValuePair<string, string>(string.Empty, string.Empty)
            };
            _appUserManager.CreateInfo(createInfoDto, Arg.Any<ClaimsPrincipal>())
                .Returns(createInfoResult);

            //Act
            var result = await _securityService.ManageCreateUserInfo(createInfoDto, new ClaimsPrincipal(), CancellationToken.None);

            //Assert
            await _appUserManager.Received(1).CreateInfo(createInfoDto, Arg.Any<ClaimsPrincipal>());

            result.IsSucc.Should().BeTrue();
            result.IfSucc(r => r.Email.Should().Be(createInfoResult.Email));
        }

        [Fact]
        public async Task LogoutUser_CallsSignOutAsync()
        {
            //Act
            await _securityService.LogoutUser(CancellationToken.None);

            //Assert
            await _appSignInManager.Received(1).SignOutAsync();
        }

        [Fact]
        public async Task GenerateTwoFactorCode_ReturnsCodeAsync()
        {
            //Arrange
            var claims = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()) }));
            _appUserManager.GenerateTwoFactorCodeAsync(Arg.Any<ClaimsPrincipal>())
                .Returns("code");

            //Act
            var result = await _securityService.GenerateTwoFactorCode(claims, CancellationToken.None);

            //Assert
            await _appUserManager.Received(1).GenerateTwoFactorCodeAsync(Arg.Any<ClaimsPrincipal>());

            result.Should().Be("code");
        }

        [Fact]
        public async Task DeleteUserAsync_AppUserManagerDeleteFails_ReturnFailAsync()
        {
            //Arrange
            var userId = Guid.NewGuid();
            _appUserManager.DeleteUserAsync(userId)
                .Returns(false);

            //Act
            var result = await _securityService.DeleteUserAsync(userId, CancellationToken.None);

            //Assert
            await _appUserManager.Received(1).DeleteUserAsync(userId);

            result.IsFail.Should().BeTrue();
            result.IfFail(e => e.ToException().Should().BeOfType<DeleteUserException>());
        }

        [Fact]
        public async Task DeleteUserAsync_AppUserManagerDeleteSucceeds_ReturnTrueAsync()
        {
            //Arrange
            var userId = Guid.NewGuid();
            _appUserManager.DeleteUserAsync(userId)
                .Returns(true);

            //Act
            var result = await _securityService.DeleteUserAsync(userId, CancellationToken.None);

            //Assert
            await _appUserManager.Received(1).DeleteUserAsync(userId);

            result.IsSucc.Should().BeTrue();
        }
    }
}
