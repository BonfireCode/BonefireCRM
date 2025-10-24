using AutoFixture;
using BonefireCRM.Domain.DTOs.Company;
using BonefireCRM.Domain.Entities;
using BonefireCRM.Domain.Infrastructure.Persistance;
using BonefireCRM.Domain.Services;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace BonefireCRM.Domain.Tests
{
    public class CompanyServiceTests
    {
        private readonly IBaseRepository<Company> _companyRepository;
        private readonly IFixture _fixture;
        private readonly CompanyService _companyService;

        public CompanyServiceTests()
        {
            _companyRepository = Substitute.For<IBaseRepository<Company>>();
            _fixture = new Fixture();

            _companyService = new CompanyService(_companyRepository);
        }

        [Fact]
        public async Task GetCompanyAsync_NoCompanyFound_ReturnNoneAsync()
        {
            //Arrange
            var id = _fixture.Create<Guid>();

            _companyRepository.GetAsync(Arg.Any<Guid>(), CancellationToken.None)
                .ReturnsNullForAnyArgs();

            //Act
            var result = await _companyService.GetCompanyAsync(id, CancellationToken.None);

            //Assert
            await _companyRepository.Received(1).GetAsync(Arg.Any<Guid>(), CancellationToken.None);

            result.IsNone.Should().BeTrue();
        }

        [Fact]
        public async Task GetCompanyAsync_CompanyFound_ReturnCompanyAsync()
        {
            //Arrange
            var id = _fixture.Create<Guid>();
            var company = _fixture.Build<Company>()
                .With(c => c.Id, id)
                .Create();

            _companyRepository.GetAsync(id, CancellationToken.None).Returns(company);

            var expected = _fixture.Build<GetCompanyDTO>()
                .With(dto => dto.Id, id)
                .With(dto => dto.Name, company.Name)
                .Create();

            //Act
            var result = await _companyService.GetCompanyAsync(id, CancellationToken.None);

            //Assert
            await _companyRepository.Received(1).GetAsync(id, CancellationToken.None);

            result.IsSome.Should().BeTrue();
            result.IfSome(dto =>
            {
                dto.Id.Should().Be(expected.Id);
                dto.Name.Should().Be(expected.Name);
            });
        }

        [Fact]
        public async Task DeleteCompanyAsync_RepositoryDeletesTrue_ReturnTrueAsync()
        {
            //Arrange
            var id = _fixture.Create<Guid>();

            _companyRepository.DeleteAsync(Arg.Any<Company>(), CancellationToken.None)
                .Returns(true);

            //Act
            var result = await _companyService.DeleteCompanyAsync(id, CancellationToken.None);

            //Assert
            await _companyRepository.Received(1).DeleteAsync(Arg.Any<Company>(), CancellationToken.None);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task CreateCompanyAsync_CreateSucceeds_ReturnCreatedDtoAsync()
        {
            //Arrange
            var id = _fixture.Create<Guid>();

            var createdCompany = _fixture.Build<Company>()
                .With(c => c.Id, id)
                .Create();

            _companyRepository.AddAsync(Arg.Any<Company>(), CancellationToken.None)
                .Returns(createdCompany);

            var createDto = _fixture.Create<CreateCompanyDTO>();

            var expected = _fixture.Build<CreatedCompanyDTO>()
                .With(dto => dto.Id, id)
                .With(dto => dto.Name, createdCompany.Name)
                .Create();

            //Act
            var result = await _companyService.CreateCompanyAsync(createDto, CancellationToken.None);

            //Assert
            await _companyRepository.Received(1).AddAsync(Arg.Any<Company>(), CancellationToken.None);

            result.IsSucc.Should().BeTrue();
            result.IfSucc(dto =>
            {
                dto.Id.Should().Be(expected.Id);
                dto.Name.Should().Be(expected.Name);
            });
        }

        [Fact]
        public async Task CreateCompanyAsync_CreateFails_ReturnFailAsync()
        {
            //Arrange
            _companyRepository.AddAsync(Arg.Any<Company>(), CancellationToken.None)
                .ReturnsNullForAnyArgs();

            var createDto = _fixture.Create<CreateCompanyDTO>();

            //Act
            var result = await _companyService.CreateCompanyAsync(createDto, CancellationToken.None);

            //Assert
            await _companyRepository.Received(1).AddAsync(Arg.Any<Company>(), CancellationToken.None);

            result.IsFail.Should().BeTrue();
        }

        [Fact]
        public async Task UpdateCompanyAsync_UpdateSucceeds_ReturnUpdatedDtoAsync()
        {
            //Arrange
            var id = _fixture.Create<Guid>();
            var updatedCompany = _fixture.Build<Company>()
                .With(c => c.Id, id)
                .Create();

            _companyRepository.UpdateAsync(Arg.Any<Company>(), CancellationToken.None)
                .Returns(updatedCompany);

            var updateDto = _fixture.Create<UpdateCompanyDTO>();

            var expected = _fixture.Build<UpdatedCompanyDTO>()
                .With(dto => dto.Id, id)
                .With(dto => dto.Name, updatedCompany.Name)
                .Create();

            //Act
            var result = await _companyService.UpdateCompanyAsync(updateDto, CancellationToken.None);

            //Assert
            await _companyRepository.Received(1).UpdateAsync(Arg.Any<Company>(), CancellationToken.None);

            result.IsSucc.Should().BeTrue();
            result.IfSucc(dto =>
            {
                dto.Id.Should().Be(expected.Id);
                dto.Name.Should().Be(expected.Name);
            });
        }

        [Fact]
        public async Task UpdateCompanyAsync_UpdateFails_ReturnFailAsync()
        {
            //Arrange
            _companyRepository.UpdateAsync(Arg.Any<Company>(), CancellationToken.None)
                .ReturnsNullForAnyArgs();

            var updateDto = _fixture.Create<UpdateCompanyDTO>();

            //Act
            var result = await _companyService.UpdateCompanyAsync(updateDto, CancellationToken.None);

            //Assert
            await _companyRepository.Received(1).UpdateAsync(Arg.Any<Company>(), CancellationToken.None);

            result.IsFail.Should().BeTrue();
        }
    }
}
