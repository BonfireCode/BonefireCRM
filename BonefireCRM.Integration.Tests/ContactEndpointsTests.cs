using Bogus;
using BonefireCRM.API.Contact.Endpoints;
using BonefireCRM.API.Contrat.Contact;
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
    public class ContactEndpointsTests : TestBase<ApiTestFixture>, IAsyncLifetime
    {
        private readonly ApiTestFixture _apiTestFixture;
        private readonly ContactTestsDataSeeder _contactTestsDataSeeder;

        public ContactEndpointsTests(ApiTestFixture apiTestFixture)
        {
            _apiTestFixture = apiTestFixture;
            _contactTestsDataSeeder = new ContactTestsDataSeeder();
        }

        public async ValueTask InitializeAsync()
        {
            await _apiTestFixture.SeedTestDatabaseAsync(_contactTestsDataSeeder.PopulateWithTestDataAsync);
        }

        [Fact]
        public async Task GetAllContact_NoFilterApplied_ReturnsAllContacts()
        {
            // Arrange
            var request = new GetContactsRequest();

            // Act
            var outcome = await _apiTestFixture.Client
                .GETAsync<GetAllContactsEndpoint, GetContactsRequest, IEnumerable<GetContactResponse>>(request);
            
            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.OK);

            outcome.Result
                .Should().NotBeNull()
                .And.NotBeEmpty()
                .And.HaveCount(_contactTestsDataSeeder.Contacts.Count);
        }

        [Fact]
        public async Task GetAllContact_OneCriteria_ReturnsAllContactsMatching()
        {
            // Arrange
            var contact = _contactTestsDataSeeder.Contacts.First();
            var request = new GetContactsRequest
            {
                Id = contact.Id,
            };

            // Act
            var outcome = await _apiTestFixture.Client
                .GETAsync<GetAllContactsEndpoint, GetContactsRequest, IEnumerable<GetContactResponse>>(request);

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.OK);

            outcome.Result.Should()
                .NotBeNull()
                .And.NotBeEmpty()
                .And.HaveCount(1);
            outcome.Result.Single().Id
                .Should().Be(contact.Id);
        }

        [Fact]
        public async Task GetAllContact_NoMatchCriteria_ReturnsNoContact()
        {
            // Arrange
            var contact = _contactTestsDataSeeder.Contacts.First();
            var request = new GetContactsRequest
            {
                JobRole = "testing",
            };

            // Act
            var outcome = await _apiTestFixture.Client
                .GETAsync<GetAllContactsEndpoint, GetContactsRequest, IEnumerable<GetContactResponse>>(request);

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.OK);

            outcome.Result
                .Should().NotBeNull()
                .And.BeEmpty();
        }

        [Fact]
        public async Task GetContact_ById_ReturnsContact()
        {
            // Arrange
            var contact = _contactTestsDataSeeder.Contacts.First();

            // Act
            var outcome = await _apiTestFixture.Client
                .GETAsync<EmptyRequest, GetContactResponse>($"/api/contacts/{contact.Id}", new());
            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.OK);

            outcome.Result
                .Should().NotBeNull();
            outcome.Result.Id
                .Should().Be(contact.Id);
            outcome.Result.FirstName
                .Should().Be(contact.FirstName);
            outcome.Result.UserId
                .Should().Be(contact.UserId);
            outcome.Result.CompanyId
                .Should().Be(contact.CompanyId!.Value);
        }

        [Fact]
        public async Task GetContact_ByNonExistingId_ReturnsNotFound()
        {
            // Arrange
            var nonExistingContactId = Guid.NewGuid();

            // Act
            var outcome = await _apiTestFixture.Client
                .GETAsync<EmptyRequest, GetContactResponse>($"/api/contacts/{nonExistingContactId}", new());
                
            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.NotFound);

            var existingContacts = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Contacts.ToListAsync());
            existingContacts
                .Should().NotContain(c => c.Id == nonExistingContactId);
        }

        [Fact]
        public async Task CreateContact_WithRequest_ReturnsCreatedContact()
        {
            // Arrange
            var lifcycleStage = _contactTestsDataSeeder.LifecycleStages.First();
            var company = _contactTestsDataSeeder.Companies.First();

            var request = new Faker<CreateContactRequest>().Rules((f, c) =>
            {
                c.FirstName = f.Name.FirstName();
                c.LastName = f.Name.LastName();
                c.Email = f.Internet.Email(c.FirstName, c.LastName);
                c.PhoneNumber = f.Phone.PhoneNumber();
                c.JobRole = f.Name.JobTitle();
                c.LifecycleStageId = lifcycleStage.Id;
                c.CompanyId = company.Id;
            })
                .Generate();

            // Act
            var outcome = await _apiTestFixture.Client
                .POSTAsync<CreateContactEndpoint, CreateContactRequest, CreateContactResponse>(request);

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.Created);

            outcome.Result
                .Should().NotBeNull()
                .And.BeEquivalentTo(request);

            var existingContacts = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Contacts.ToListAsync());
            existingContacts
                .Should().Contain(c => c.Id == outcome.Result.Id);
        }

        [Fact]
        public async Task CreateContact_WithEmptyRequest_ReturnsBadRequest()
        {
            // Arrange
            var lifcycleStage = _contactTestsDataSeeder.LifecycleStages.First();
            var company = _contactTestsDataSeeder.Companies.First();

            var request = new CreateContactRequest();

            // Act
            var outcome = await _apiTestFixture.Client
                .POSTAsync<CreateContactEndpoint, CreateContactRequest, InternalServerError>(request);

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.BadRequest);

            var existingContacts = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Contacts.ToListAsync());
            existingContacts
                .Should().HaveCount(_contactTestsDataSeeder.Contacts.Count);
        }

        [Fact]
        public async Task CreateContact_WithInvalidRequest_ReturnsError()
        {
            // Arrange
            var request = new Faker<CreateContactRequest>().Rules((f, c) =>
            {
                c.FirstName = f.Name.FirstName();
                c.LastName = f.Name.LastName();
                c.Email = f.Internet.Email(c.FirstName, c.LastName);
                c.PhoneNumber = f.Phone.PhoneNumber();
                c.JobRole = f.Name.JobTitle();
                c.LifecycleStageId = Guid.NewGuid();
                c.CompanyId = Guid.NewGuid();
            })
                .Generate();

            // Act
            var outcome = await _apiTestFixture.Client
                .POSTAsync<CreateContactEndpoint, CreateContactRequest, InternalServerError>(request);

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.InternalServerError);

            var existingContacts = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Contacts.ToListAsync());
            existingContacts
                .Should().HaveCount(_contactTestsDataSeeder.Contacts.Count);
        }

        [Fact]
        public async Task UpdateContact_WithRequest_ReturnsUpdatedContact()
        {
            // Arrange
            var lifcycleStage = _contactTestsDataSeeder.LifecycleStages.First();
            var company = _contactTestsDataSeeder.Companies.First();

            var contact = _contactTestsDataSeeder.Contacts.First();

            var request = new Faker<UpdateContactRequest>().Rules((f, c) =>
            {
                c.FirstName = f.Name.FirstName();
                c.LastName = f.Name.LastName();
                c.Email = f.Internet.Email(c.FirstName, c.LastName);
                c.PhoneNumber = f.Phone.PhoneNumber();
                c.JobRole = f.Name.JobTitle();
                c.LifecycleStageId = lifcycleStage.Id;
                c.CompanyId = company.Id;
            })
                .Generate();

            // Act
            var outcome = await _apiTestFixture.Client
                .PUTAsync<UpdateContactRequest, UpdateContactResponse>($"/api/contacts/{contact.Id}", request);
                
            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.OK);

            outcome.Result
                .Should().NotBeNull()
                .And.BeEquivalentTo(request)
                .And.NotBeEquivalentTo(contact);

            var existingContacts = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Contacts.ToListAsync());
            existingContacts
                .Should().HaveCount(_contactTestsDataSeeder.Contacts.Count)
                .And.Contain(c => c.Id == outcome.Result.Id && c.FirstName == outcome.Result.FirstName)
                .And.NotContain(c => c.FirstName == contact.FirstName && c.LastName == contact.LastName);
        }

        [Fact]
        public async Task UpdateContact_ByNonExistingId_ReturnsNotFound()
        {
            // Arrange
            var lifcycleStage = _contactTestsDataSeeder.LifecycleStages.First();
            var company = _contactTestsDataSeeder.Companies.First();

            var nonExistingContactId = Guid.NewGuid();

            var request = new Faker<UpdateContactRequest>().Rules((f, c) =>
            {
                c.FirstName = f.Name.FirstName();
                c.LastName = f.Name.LastName();
                c.Email = f.Internet.Email(c.FirstName, c.LastName);
                c.PhoneNumber = f.Phone.PhoneNumber();
                c.JobRole = f.Name.JobTitle();
                c.LifecycleStageId = lifcycleStage.Id;
                c.CompanyId = company.Id;
            })
                .Generate();

            // Act
            var outcome = await _apiTestFixture.Client
                .PUTAsync<UpdateContactRequest, UpdateContactResponse>($"/api/contacts/{nonExistingContactId}", request);

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.NotFound);

            var existingContacts = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Contacts.ToListAsync());
            existingContacts
                .Should().HaveCount(_contactTestsDataSeeder.Contacts.Count)
                .And.NotContain(c => c.Id == nonExistingContactId && c.LastName == request.FirstName);
        }

        [Fact]
        public async Task DeleteContact_ById_ReturnsNoContentAndDeletes()
        {
            // Arrange
            var contact = _contactTestsDataSeeder.Contacts.First();
            
            // Act
            var outcome = await _apiTestFixture.Client
                .DELETEAsync<EmptyRequest, NoContent>($"/api/contacts/{contact.Id}", new());

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.NoContent);

            var existingContacts = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Contacts.ToListAsync());
            existingContacts
                .Should().HaveCount(_contactTestsDataSeeder.Contacts.Count - 1)
                .And.NotContain(c => c.Id == contact.Id);
        }

        [Fact]
        public async Task DeleteContact_ByNonExistingId_ReturnsNotFound()
        {
            // Arrange
            var nonExistingContactId = Guid.NewGuid();
            // Act
            var outcome = await _apiTestFixture.Client
                .DELETEAsync<EmptyRequest, NoContent>($"/api/contacts/{nonExistingContactId}", new());

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.NotFound);

            var existingContacts = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Contacts.ToListAsync());
            existingContacts.Count
                .Should().Be(_contactTestsDataSeeder.Contacts.Count);
            existingContacts
                .Should().NotContain(c => c.Id == nonExistingContactId);
        }

        public async ValueTask DisposeAsync()
        {
            await _apiTestFixture.ResetDatabaseAsync();
        }
    }
}
