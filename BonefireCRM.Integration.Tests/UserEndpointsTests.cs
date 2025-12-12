using Bogus;
using BonefireCRM.API.Contrat.Security;
using BonefireCRM.API.Contrat.User;
using BonefireCRM.API.Security.Endpoints;
using BonefireCRM.API.User.Endpoints;
using BonefireCRM.Integration.Tests.Common;
using BonefireCRM.Integration.Tests.DataSeeders;
using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BonefireCRM.Integration.Tests
{
    public class UserEndpointsTests : TestBase<ApiTestFixture>, IAsyncLifetime
    {
        private readonly ApiTestFixture _apiTestFixture;
        private readonly UserTestsDataSeeder _userTestsDataSeeder;

        public UserEndpointsTests(ApiTestFixture apiTestFixture)
        {
            _apiTestFixture = apiTestFixture;
            _userTestsDataSeeder = new UserTestsDataSeeder();
        }

        public async ValueTask InitializeAsync()
        {
            await _apiTestFixture.SeedTestDatabaseAsync(_userTestsDataSeeder.PopulateWithTestDataAsync);
        }

        [Fact]
        public async Task GetAllUsers_NoFilterApplied_ReturnsAllUsers()
        {
            // Arrange
            var request = new GetUsersRequest();

            // Act
            var outcome = await _apiTestFixture.Client
                .GETAsync<GetAllUsersEndpoint, GetUsersRequest, IEnumerable<GetUserResponse>>(request);

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.OK);

            outcome.Result
                .Should().NotBeNull()
                .And.NotBeEmpty()
                .And.HaveCount(_userTestsDataSeeder.Users.Count);
        }

        [Fact]
        public async Task GetAllUsers_OneCriteria_ReturnsAllUsersMatching()
        {
            // Arrange
            var user = _userTestsDataSeeder.Users.First();
            var request = new GetUsersRequest
            {
                Id = user.Id,
            };

            // Act
            var outcome = await _apiTestFixture.Client
                .GETAsync<GetAllUsersEndpoint, GetUsersRequest, IEnumerable<GetUserResponse>>(request);

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.OK);

            outcome.Result.Should()
                .NotBeNull()
                .And.NotBeEmpty()
                .And.HaveCount(_userTestsDataSeeder.Users.Count);
            outcome.Result.Single().Id
                .Should().Be(user.Id);
        }

        [Fact]
        public async Task GetAllUsers_NoMatchCriteria_ReturnsNoUser()
        {
            // Arrange
            var request = new GetUsersRequest
            {
                UserName = "testing",
            };

            // Act
            var outcome = await _apiTestFixture.Client
                .GETAsync<GetAllUsersEndpoint, GetUsersRequest, IEnumerable<GetUserResponse>>(request);

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.OK);

            outcome.Result
                .Should().NotBeNull()
                .And.BeEmpty();
        }

        [Fact]
        public async Task GetUser_ById_ReturnsUser()
        {
            // Arrange
            var user = _userTestsDataSeeder.Users.First();

            // Act
            var outcome = await _apiTestFixture.Client
                .GETAsync<EmptyRequest, GetUserResponse>($"/api/users/{user.Id}", new());

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.OK);

            outcome.Result
                .Should().NotBeNull();
            outcome.Result.Id
                .Should().Be(user.Id);
            outcome.Result.FirstName
                .Should().Be(user.FirstName);
            outcome.Result.Email
                .Should().Be(user.Email);
        }

        [Fact]
        public async Task GetUser_ByNonExistingId_ReturnsNotFound()
        {
            // Arrange
            var nonExistingUserId = Guid.NewGuid();

            // Act
            var outcome = await _apiTestFixture.Client
                .GETAsync<EmptyRequest, GetUserResponse>($"/api/users/{nonExistingUserId}", new());

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.NotFound);

            var existingUsers = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Users.ToListAsync());
            existingUsers
                .Should().NotContain(u => u.Id == nonExistingUserId);
        }

        [Fact]
        public async Task CreateUser_OnRegisterationWithRequest_ReturnsCreatedUser()
        {
            // Arrange
            await _apiTestFixture.ResetDatabaseAsync();

            var request = new Faker<RegisterRequest>()
                .Rules((f, u) =>
            {
                u.UserName = f.Internet.UserName();
                u.Password = f.Internet.Password();
                u.Email = f.Internet.Email(u.UserName);
            })
                .Generate();

            // Act
            var outcome = await _apiTestFixture.Client
                .POSTAsync<RegisterEndpoint, RegisterRequest, Guid>(request);

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.OK);

            var existingUsers = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Users.ToListAsync());
            existingUsers
                .Should().HaveCount(1)
                .And.Contain(u => u.Id == outcome.Result);

            var existingAppUsers = await _apiTestFixture.ExecuteScopedAppDbContextAsync(c => c.Users.ToListAsync());
            existingAppUsers
                .Should().HaveCount(1);
        }

        [Fact]
        public async Task CreateUser_WithInvalidRequest_ReturnsError()
        {
            // Arrange
            var request = new Faker<RegisterRequest>().Rules((f, u) =>
            {
                u.UserName = f.Internet.UserName();
                u.Password = f.Internet.Password();
                u.Email = "invalid-email";
            })
                .Generate();

            // Act
            var outcome = await _apiTestFixture.Client
                .POSTAsync<RegisterEndpoint, RegisterRequest, ProblemHttpResult>(request);

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.BadRequest);

            var existingUsers = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Users.ToListAsync());
            existingUsers
                .Should().HaveCount(_userTestsDataSeeder.Users.Count);
        }

        [Fact]
        public async Task UpdateUser_WithRequest_ReturnsUpdatedUser()
        {
            // Arrange
            var user = _userTestsDataSeeder.Users.First();

            var request = new Faker<UpdateUserRequest>().Rules((f, u) =>
            {
                u.Email = f.Internet.Email();
                u.FirstName = f.Name.FirstName();
                u.LastName = f.Name.LastName();
            })
                .Generate();

            // Act
            var outcome = await _apiTestFixture.Client
                .PUTAsync<UpdateUserRequest, UpdateUserResponse>($"/api/users/{user.Id}", request);

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.OK);

            outcome.Result
                .Should().NotBeNull()
                .And.BeEquivalentTo(request)
                .And.NotBeEquivalentTo(user);

            var existingUsers = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Users.ToListAsync());
            existingUsers
                .Should().HaveCount(_userTestsDataSeeder.Users.Count)
                .And.Contain(u => u.Id == outcome.Result.Id && u.FirstName == outcome.Result.FirstName)
                .And.NotContain(u => u.FirstName == user.FirstName && u.LastName == user.LastName && u.Id == user.Id);
        }

        [Fact]
        public async Task UpdateUser_ByNonExistingId_ReturnsNotFound()
        {
            // Arrange
            var nonExistingUserId = Guid.NewGuid();

            var request = new Faker<UpdateUserRequest>().Rules((f, u) =>
            {
                u.Email = f.Internet.Email();
                u.FirstName = f.Name.FirstName();
                u.LastName = f.Name.LastName();
            })
                .Generate();

            // Act
            var outcome = await _apiTestFixture.Client
                .PUTAsync<UpdateUserRequest, UpdateUserResponse>($"/api/users/{nonExistingUserId}", request);

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.InternalServerError);

            var existingUsers = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Users.ToListAsync());
            existingUsers
                .Should().HaveCount(_userTestsDataSeeder.Users.Count)
                .And.NotContain(u => u.Id == nonExistingUserId && u.LastName == request.LastName);
        }

        [Fact]
        public async Task DeleteUser_ById_ReturnsNoContentAndDeleteRegistredUser()
        {
            // Arrange
            var user = _userTestsDataSeeder.Users.First();

            // Act
            var outcome = await _apiTestFixture.Client
                .DELETEAsync<EmptyRequest, NoContent>($"/api/users/{user.Id}", new());

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.NoContent);

            var existingUsers = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Users.ToListAsync());
            existingUsers
                .Should().BeEmpty();

            var existingAppUsers = await _apiTestFixture.ExecuteScopedAppDbContextAsync(c => c.Users.ToListAsync());
            existingAppUsers
                .Should().BeEmpty();
        }

        [Fact]
        public async Task DeleteUser_ByNonExistingId_ReturnsNotFound()
        {
            // Arrange
            var nonExistingUserId = Guid.NewGuid();

            // Act
            var outcome = await _apiTestFixture.Client
                .DELETEAsync<EmptyRequest, NoContent>($"/api/users/{nonExistingUserId}", new());

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.NotFound);

            var existingUsers = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Users.ToListAsync());
            existingUsers
                .Should().HaveCount(_userTestsDataSeeder.Users.Count);
            existingUsers
                .Should().NotContain(u => u.Id == nonExistingUserId);

            var existingAppUsers = await _apiTestFixture.ExecuteScopedAppDbContextAsync(c => c.Users.ToListAsync());
            existingAppUsers
                .Should().HaveCount(_userTestsDataSeeder.Users.Count);
        }

        public async ValueTask DisposeAsync()
        {
            await _apiTestFixture.ResetDatabaseAsync();
        }
    }
}
