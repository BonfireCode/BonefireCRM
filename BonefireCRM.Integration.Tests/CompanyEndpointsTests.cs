using Bogus;
using BonefireCRM.API.Company.Endpoints;
using BonefireCRM.API.Contrat.Company;
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
    public class CompanyEndpointsTests : TestBase<ApiTestFixture>, IAsyncLifetime
    {
        private readonly ApiTestFixture _apiTestFixture;
        private readonly CompanyTestsDataSeeder _companyTestsDataSeeder;

        public CompanyEndpointsTests(ApiTestFixture apiTestFixture)
        {
            _apiTestFixture = apiTestFixture;
            _companyTestsDataSeeder = new CompanyTestsDataSeeder();
        }

        public async ValueTask InitializeAsync()
        {
            await _apiTestFixture.SeedTestDatabaseAsync(_companyTestsDataSeeder.PopulateWithTestDataAsync);
        }

        [Fact]
        public async Task GetAllCompanies_NoFilterApplied_ReturnsAllCompanies()
        {
            // Arrange
            var request = new GetCompaniesRequest();

            // Act
            var outcome = await _apiTestFixture.Client
                .GETAsync<GetAllCompaniesEndpoint, GetCompaniesRequest, IEnumerable<GetCompanyResponse>>(request);

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.OK);

            outcome.Result
                .Should().NotBeNull()
                .And.NotBeEmpty()
                .And.HaveCount(_companyTestsDataSeeder.Companies.Count);
        }

        [Fact]
        public async Task GetAllCompanies_OneCriteria_ReturnsAllCompaniesMatching()
        {
            // Arrange
            var company = _companyTestsDataSeeder.Companies.First();
            var request = new GetCompaniesRequest
            {
                Id = company.Id,
            };

            // Act
            var outcome = await _apiTestFixture.Client
                .GETAsync<GetAllCompaniesEndpoint, GetCompaniesRequest, IEnumerable<GetCompanyResponse>>(request);

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.OK);

            outcome.Result.Should()
                .NotBeNull()
                .And.NotBeEmpty()
                .And.HaveCount(1);
            outcome.Result.Single().Id
                .Should().Be(company.Id);
        }

        [Fact]
        public async Task GetAllCompanies_NoMatchCriteria_ReturnsNoCompanies()
        {
            // Arrange
            var request = new GetCompaniesRequest
            {
                Name = "testing",
            };

            // Act
            var outcome = await _apiTestFixture.Client
                .GETAsync<GetAllCompaniesEndpoint, GetCompaniesRequest, IEnumerable<GetCompanyResponse>>(request);

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.OK);

            outcome.Result
                .Should().NotBeNull()
                .And.BeEmpty();
        }

        [Fact]
        public async Task GetCompany_ById_ReturnsCompany()
        {
            // Arrange
            var company = _companyTestsDataSeeder.Companies.First();

            // Act
            var outcome = await _apiTestFixture.Client
                .GETAsync<EmptyRequest, GetCompanyResponse>($"/api/companies/{company.Id}", new());

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.OK);

            outcome.Result
                .Should().NotBeNull();
            outcome.Result.Id
                .Should().Be(company.Id);
            outcome.Result.Name
                .Should().Be(company.Name);
            outcome.Result.Industry
                .Should().Be(company.Industry);
        }

        [Fact]
        public async Task GetCompany_ByNonExistingId_ReturnsNotFound()
        {
            // Arrange
            var nonExistingCompanyId = Guid.NewGuid();

            // Act
            var outcome = await _apiTestFixture.Client
                .GETAsync<EmptyRequest, GetCompanyResponse>($"/api/companies/{nonExistingCompanyId}", new());

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.NotFound);

            var existingCompanies = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Companies.ToListAsync());
            existingCompanies
                .Should().NotContain(c => c.Id == nonExistingCompanyId);
        }

        [Fact]
        public async Task CreateCompany_WithRequest_ReturnsCreatedCompany()
        {
            // Arrange
            var company = _companyTestsDataSeeder.Companies.First();

            var request = new Faker<CreateCompanyRequest>().Rules((f, c) =>
            {
                c.Name = f.Company.CompanyName();
                c.Industry = f.Commerce.Department();
                c.Address = f.Address.FullAddress();
                c.PhoneNumber = f.Phone.PhoneNumber();
            })
                .Generate();

            // Act
            var outcome = await _apiTestFixture.Client
                .POSTAsync<CreateCompanyEndpoint, CreateCompanyRequest, CreateCompanyResponse>(request);

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.Created);

            outcome.Result
                .Should().NotBeNull()
                .And.BeEquivalentTo(request);

            var existingCompanies = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Companies.ToListAsync());
            existingCompanies
                .Should().Contain(c => c.Id == outcome.Result.Id);
        }

        [Fact]
        public async Task CreateCompany_WithEmptyRequest_ReturnsBadRequest()
        {
            // Arrange
            var request = new CreateCompanyRequest();

            // Act
            var outcome = await _apiTestFixture.Client
                .POSTAsync<CreateCompanyEndpoint, CreateCompanyRequest, InternalServerError>(request);

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.BadRequest);

            var existingCompanies = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Companies.ToListAsync());
            existingCompanies
                .Should().HaveCount(_companyTestsDataSeeder.Companies.Count);
        }

        [Fact]
        public async Task UpdateCompany_WithRequest_ReturnsUpdatedCompany()
        {
            // Arrange
            var company = _companyTestsDataSeeder.Companies.First();

            var request = new Faker<UpdateCompanyRequest>().Rules((f, c) =>
            {
                c.Name = f.Company.CompanyName();
                c.Industry = f.Commerce.Department();
                c.Address = f.Address.FullAddress();
                c.PhoneNumber = f.Phone.PhoneNumber();
            })
                .Generate();

            // Act
            var outcome = await _apiTestFixture.Client
                .PUTAsync<UpdateCompanyRequest, UpdateCompanyResponse>($"/api/companies/{company.Id}", request);

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.OK);

            outcome.Result
                .Should().NotBeNull()
                .And.BeEquivalentTo(request)
                .And.NotBeEquivalentTo(company);

            var existingCompanies = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Companies.ToListAsync());
            existingCompanies
                .Should().HaveCount(_companyTestsDataSeeder.Companies.Count)
                .And.Contain(c => c.Id == outcome.Result.Id && c.Name == outcome.Result.Name);
        }

        [Fact]
        public async Task UpdateCompany_ByNonExistingId_ReturnsNotFound()
        {
            // Arrange
            var nonExistingCompanyId = Guid.NewGuid();
            var request = new Faker<UpdateCompanyRequest>().Rules((f, c) =>
            {
                c.Name = f.Company.CompanyName();
                c.Industry = f.Commerce.Department();
                c.Address = f.Address.FullAddress();
                c.PhoneNumber = f.Phone.PhoneNumber();
            })
                .Generate();

            // Act
            var outcome = await _apiTestFixture.Client
                .PUTAsync<UpdateCompanyRequest, UpdateCompanyResponse>($"/api/companies/{nonExistingCompanyId}", request);

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.NotFound);

            var existingCompanies = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Companies.ToListAsync());
            existingCompanies
                .Should().HaveCount(_companyTestsDataSeeder.Companies.Count)
                .And.NotContain(c => c.Id == nonExistingCompanyId && c.Name == request.Name);
        }

        [Fact]
        public async Task DeleteCompany_ById_ReturnsNoContentAndDeletes()
        {
            // Arrange
            var company = _companyTestsDataSeeder.Companies.First();

            // Act
            var outcome = await _apiTestFixture.Client
                .DELETEAsync<EmptyRequest, NoContent>($"/api/companies/{company.Id}", new());

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.NoContent);

            var existingCompanies = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Companies.ToListAsync());
            existingCompanies
                .Should().HaveCount(_companyTestsDataSeeder.Companies.Count - 1)
                .And.NotContain(c => c.Id == company.Id);
        }

        [Fact]
        public async Task DeleteCompany_ByNonExistingId_ReturnsNotFound()
        {
            // Arrange
            var nonExistingCompanyId = Guid.NewGuid();

            // Act
            var outcome = await _apiTestFixture.Client
                .DELETEAsync<EmptyRequest, NoContent>($"/api/companies/{nonExistingCompanyId}", new());

            // Assert
            outcome.Response.StatusCode
                .Should().Be(HttpStatusCode.NotFound);

            var existingCompanies = await _apiTestFixture.ExecuteScopedDbContextAsync(c => c.Companies.ToListAsync());
            existingCompanies.Count
                .Should().Be(_companyTestsDataSeeder.Companies.Count);
            existingCompanies
                .Should().NotContain(c => c.Id == nonExistingCompanyId);
        }

        public async ValueTask DisposeAsync()
        {
            await _apiTestFixture.ResetDatabaseAsync();
        }
    }
}
