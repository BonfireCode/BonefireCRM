//using Bogus;
//using BonefireCRM.API.User.Endpoints;
//using BonefireCRM.API.Contrat.User;
//using BonefireCRM.Integration.Tests.Common;
//using BonefireCRM.Integration.Tests.DataSeeders;
//using FastEndpoints;
//using FluentAssertions;
//using Microsoft.EntityFrameworkCore;
//using System.Net;

//namespace BonefireCRM.Integration.Tests
//{
//    public class UserEndpointsTests : IAsyncLifetime
//    {
//        private readonly ApiTestFixture _apiTestFixture;
//        private readonly ContactTestsDataSeeder _contactTestsDataSeeder;

//        public UserEndpointsTests(ApiTestFixture apiTestFixture)
//        {
//            _apiTestFixture = apiTestFixture;
//            _contactTestsDataSeeder = new ContactTestsDataSeeder();
//        }

//        public async ValueTask InitializeAsync()
//        {
//            await _apiTestFixture.SeedTestDatabaseAsync(_contactTestsDataSeeder.PopulateWithTestDataAsync);
//        }

//        [Fact]
//        public async Task GetAllUsers_NoFilterApplied_ReturnsAllUsers()
//        {
//            // Arrange
//            var request = new GetUsersRequest();

//            // Act
//            var outcome = await _apiTestFixture.Client
//                .GETAsync<GetAllUsersEndpoint, GetUsersRequest, IEnumerable<GetUserResponse>>(request);

//            // Assert
//            outcome.Response.StatusCode
//                .Should().Be(HttpStatusCode.OK);

//            outcome.Result
//                .Should().NotBeNull()
//                .And.NotBeEmpty()
//                .And.HaveCount(_contactTestsDataSeeder.Users.Count);
//        }

//        [Fact]
//        public async Task GetAllUsers_OneCriteria_ReturnsAllUsersMatching()
//        {
//            // Arrange
//            var user = _contactTestsDataSeeder.Users.First();
//            var request = new GetUsersRequest
//            {
//                Id = user.Id,
//            };

//            // Act
//            var outcome = await _apiTestFixture.Client
//                .GETAsync<GetAllUsersEndpoint, GetUsersRequest, IEnumerable<GetUserResponse>>(request);

//            // Assert
//            outcome.Response.StatusCode
//                .Should().Be(HttpStatusCode.OK);

//            outcome.Result.Should()
//                .NotBeNull()
//                .And.NotBeEmpty()
//                .And.HaveCount(1);
//            outcome.Result.Single().Id
//                .Should().Be(user.Id);
//        }

//        [Fact]
//        public async Task GetUser_ById_ReturnsUser()
//        {
//            // Arrange
//            var user = _contactTestsDataSeeder.Users.First();

//            // Act
//            var outcome = await _apiTestFixture.Client
//                .GETAsync<EmptyRequest, GetUserResponse>($"/api/users/{user.Id}", new());

//            // Assert
//            outcome.Response.StatusCode
//                .Should().Be(HttpStatusCode.OK);

//            outcome.Result
//                .Should().NotBeNull();
//            outcome.Result.Id
//                .Should().Be(user.Id);
//            outcome.Result.UserName
//                .Should().Be(user.UserName);
//            outcome.Result.Email
//                .Should().Be(user.Email);
//        }

//        [Fact]
//        public async Task CreateUser_WithRequest_ReturnsCreatedUser()
//        {
//            // Arrange
//            var user = _contactTestsDataSeeder.Users.First();

//            var request = new Faker<CreateUserRequest>().Rules((f, u) =>
//            {
//                u.UserName = f.Internet.UserName();
//                u.Email = f.Internet.Email();
//                u.FirstName = f.Name.FirstName();
//                u.LastName = f.Name.LastName();
//            })
//                .Generate();

//            // Act
//            var outcome = await _apiTestFixture.Client
//                .POSTAsync<CreateUserEndpoint, CreateUserRequest, CreateUserResponse>(request);

//            // Assert
//            outcome.Response.StatusCode
//                .Should().Be(HttpStatusCode.Created);

//            outcome.Result
//                .Should().NotBeNull()
//                .And.BeEquivalentTo(request);

//            var existingUsers = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Users.ToListAsync());
//            existingUsers
//                .Should().Contain(u => u.Id == outcome.Result.Id);
//        }

//        [Fact]
//        public async Task UpdateUser_WithRequest_ReturnsUpdatedUser()
//        {
//            // Arrange
//            var user = _contactTestsDataSeeder.Users.First();

//            var request = new Faker<UpdateUserRequest>().Rules((f, u) =>
//            {
//                u.UserName = f.Internet.UserName();
//                u.Email = f.Internet.Email();
//                u.FirstName = f.Name.FirstName();
//                u.LastName = f.Name.LastName();
//            })
//                .Generate();

//            // Act
//            var outcome = await _apiTestFixture.Client
//                .PUTAsync<UpdateUserRequest, UpdateUserResponse>($"/api/users/{user.Id}", request);

//            // Assert
//            outcome.Response.StatusCode
//                .Should().Be(HttpStatusCode.OK);

//            outcome.Result
//                .Should().NotBeNull()
//                .And.BeEquivalentTo(request)
//                .And.NotBeEquivalentTo(user);

//            var existingUsers = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Users.ToListAsync());
//            existingUsers
//                .Should().HaveCount(_contactTestsDataSeeder.Users.Count)
//                .And.Contain(u => u.Id == outcome.Result.Id && u.UserName == outcome.Result.UserName);
//        }

//        public async ValueTask DisposeAsync()
//        {
//            await _apiTestFixture.ResetDatabaseAsync();
//        }
//    }
//}
