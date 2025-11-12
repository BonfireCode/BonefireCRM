using AutoFixture;
using BonefireCRM.Domain.Constants;
using BonefireCRM.Domain.DTOs.Contact;
using BonefireCRM.Domain.Entities;
using BonefireCRM.Domain.Exceptions;
using BonefireCRM.Domain.Infrastructure.Persistance;
using BonefireCRM.Domain.Services;
using BonefireCRM.SourceGenerator;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System.Linq.Expressions;

namespace BonefireCRM.Domain.Tests
{
    public class ContactServiceTests
    {
        private readonly IBaseRepository<Contact> _contactRepository;
        private readonly IFixture _fixture;
        private readonly ContactService _contactService;

        public ContactServiceTests()
        {
            _contactRepository = Substitute.For<IBaseRepository<Contact>>();
            _fixture = new Fixture();

            _contactService = new ContactService(_contactRepository);
        }

        [Fact]
        public async Task GetContactAsync_NoContactFound_ReturnNoneAsync()
        {
            // Arange
            var id = _fixture.Create<Guid>();

            _contactRepository.GetAsync(Arg.Any<Guid>(), CancellationToken.None)
                .ReturnsNullForAnyArgs();

            // Act
            var result = await _contactService.GetContactAsync(id, CancellationToken.None);

            // Assert
            await _contactRepository.Received(1).GetAsync(Arg.Any<Guid>(), CancellationToken.None);

            result.IsNone.Should().BeTrue();
        }

        [Fact]
        public async Task GetContactAsync_ContactFound_ReturnContactAsync()
        {
            // Arange
            var id = _fixture.Create<Guid>();
            var contact = _fixture.Build<Contact>()
                .With(c => c.Id, id)
                .Create();
            _contactRepository.GetAsync(id, CancellationToken.None)
                .Returns(contact);

            var expectedContact = _fixture.Build<GetContactDTO>()
                .With(dto => dto.Id, id)
                .With(dto => dto.FirstName, contact.FirstName)
                .With(dto => dto.LastName, contact.LastName)
                .Create();

            // Act
            var result = await _contactService.GetContactAsync(id, CancellationToken.None);

            // Assert
            await _contactRepository.Received(1).GetAsync(id, CancellationToken.None);

            result.IsSome.Should().BeTrue();
            result.IfSome(contactDto =>
            {
                contactDto.Id.Should().Be(expectedContact.Id);
                contactDto.FirstName.Should().Be(expectedContact.FirstName);
                contactDto.LastName.Should().Be(expectedContact.LastName);
            });
        }

        [Fact]
        public async Task GetAllContacts_NoContactsFound_ReturnEmptyEnumerable()
        {
            // Arange
            var getAllContactsDTO = _fixture.Build<GetAllContactsDTO>()
                .OmitAutoProperties()
                .With(dto => dto.SortBy, DefaultValues.SORTBY)
                .With(dto => dto.SortDirection, DefaultValues.SORTDIRECTION)
                .With(dto => dto.PageNumber, DefaultValues.PAGENUMBER)
                .With(dto => dto.PageSize, DefaultValues.PAGESIZE)
                .Create();

            var filterExpression = ContactQueryExpressions.Filter(getAllContactsDTO);
            var sortExpression = ContactQueryExpressions.Sort(getAllContactsDTO.SortBy);
            var skip = (getAllContactsDTO.PageNumber - 1) * getAllContactsDTO.PageSize;
            var take = getAllContactsDTO.PageSize;
            _contactRepository.GetAll(
                    Arg.Is<Expression<Func<Contact, bool>>>(e => filterExpression.Body.ToString() == e.Body.ToString()),
                    Arg.Is<Expression<Func<Contact, object>>>(e => sortExpression.Body.ToString() == e.Body.ToString()),
                    getAllContactsDTO.SortDirection,
                    skip,
                    take,
                    CancellationToken.None)
                .Returns([]);

            // Act
            var result = _contactService.GetAllContacts(getAllContactsDTO, CancellationToken.None);

            // Assert
            _contactRepository.Received(1).GetAll(
                    Arg.Is<Expression<Func<Contact, bool>>>(e => filterExpression.Body.ToString() == e.Body.ToString()),
                    Arg.Is<Expression<Func<Contact, object>>>(e => sortExpression.Body.ToString() == e.Body.ToString()),
                    getAllContactsDTO.SortDirection,
                    skip,
                    take,
                    CancellationToken.None);

            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllContacts_OneContactFound_ReturnEnumerableWithContact()
        {
            // Arange
            var getAllContactsDTO = _fixture.Build<GetAllContactsDTO>()
                .OmitAutoProperties()
                .With(dto => dto.SortBy, DefaultValues.SORTBY)
                .With(dto => dto.SortDirection, DefaultValues.SORTDIRECTION)
                .Create();

            var contact = _fixture.Create<Contact>();

            var filterExpression = ContactQueryExpressions.Filter(getAllContactsDTO);
            var sortExpression = ContactQueryExpressions.Sort(getAllContactsDTO.SortBy);
            var skip = (getAllContactsDTO.PageNumber - 1) * getAllContactsDTO.PageSize;
            var take = getAllContactsDTO.PageSize;
            _contactRepository.GetAll(
                    Arg.Is<Expression<Func<Contact, bool>>>(e => filterExpression.Body.ToString() == e.Body.ToString()),
                    Arg.Is<Expression<Func<Contact, object>>>(e => sortExpression.Body.ToString() == e.Body.ToString()),
                    getAllContactsDTO.SortDirection,
                    skip,
                    take,
                    CancellationToken.None)
                .Returns([contact]);

            var expectedContact = _fixture.Build<GetContactDTO>()
                .With(dto => dto.Id, contact.Id)
                .With(dto => dto.FirstName, contact.FirstName)
                .With(dto => dto.LastName, contact.LastName)
                .With(dto => dto.UserId, contact.UserId)
                .Create();

            // Act
            var result = _contactService.GetAllContacts(getAllContactsDTO, CancellationToken.None);

            // Assert
            _contactRepository.Received(1).GetAll(
                    Arg.Is<Expression<Func<Contact, bool>>>(e => filterExpression.Body.ToString() == e.Body.ToString()),
                    Arg.Is<Expression<Func<Contact, object>>>(e => sortExpression.Body.ToString() == e.Body.ToString()),
                    getAllContactsDTO.SortDirection,
                    skip,
                    take,
                    CancellationToken.None);

            result.Should().HaveCount(1);
            var element = result.Single();
            element.Id.Should().Be(expectedContact.Id);
            element.FirstName.Should().Be(expectedContact.FirstName);
            element.LastName.Should().Be(expectedContact.LastName);
            element.UserId.Should().Be(expectedContact.UserId);
        }

        [Fact]
        public async Task DeleteContactAsync_ContactFound_ReturnTrueAsync()
        {
            // Arange
            _contactRepository.DeleteAsync(Arg.Any<Contact>(), CancellationToken.None)
                .Returns(true);

            // Act
            var result = await _contactService.DeleteContactAsync(new(), CancellationToken.None);

            // Assert
            await _contactRepository.Received(1).DeleteAsync(Arg.Any<Contact>(), CancellationToken.None);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteContactAsync_ContactFound_ReturnFalseAsync()
        {
            // Arange
            _contactRepository.DeleteAsync(Arg.Any<Contact>(), CancellationToken.None)
                .Returns(false);

            // Act
            var result = await _contactService.DeleteContactAsync(new(), CancellationToken.None);

            // Assert
            await _contactRepository.Received(1).DeleteAsync(Arg.Any<Contact>(), CancellationToken.None);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task CreateContactAsync_NoContactCreated_ReturnAddEntityExceptionAsync()
        {
            // Arange
            var createContactDTO = _fixture.Create<CreateContactDTO>();
            _contactRepository.AddAsync(Arg.Any<Contact>(), CancellationToken.None)
                .ReturnsNullForAnyArgs();

            // Act
            var result = await _contactService.CreateContactAsync(createContactDTO, CancellationToken.None);

            // Assert
            await _contactRepository.Received(1).AddAsync(Arg.Any<Contact>(), CancellationToken.None);

            result.IsFail.Should().BeTrue();
            result.IfFail(error =>
            {
                error.IsExceptional.Should().BeTrue();
                error.ToException().Should().BeOfType<AddEntityException<Contact>>();
            });
        }

        [Fact]
        public async Task CreateContactAsync_ContactCreated_ReturnContactAsync()
        {
            // Arange
            var id = _fixture.Create<Guid>();

            var createContactDTO = _fixture.Create<CreateContactDTO>();
            var contact = _fixture.Build<Contact>()
                .Without(c => c.Id)
                .With(c => c.FirstName, createContactDTO.FirstName)
                .With(c => c.LastName, createContactDTO.LastName)
                .With(c => c.UserId, createContactDTO.UserId)
                .Create();

            _contactRepository
                .AddAsync(Arg.Is<Contact>(c => c.FirstName == contact.FirstName), CancellationToken.None)
                .Returns(call =>
                {
                    var arg = call.Arg<Contact>();
                    arg.Id = id;
                    return arg;
                });

            var expectedContact = _fixture.Build<CreatedContactDTO>()
                .With(c => c.Id, id)
                .With(c => c.FirstName, contact.FirstName)
                .With(c => c.LastName, contact.LastName)
                .With(c => c.UserId, contact.UserId)
                .Create();

            // Act
            var result = await _contactService.CreateContactAsync(createContactDTO, CancellationToken.None);

            // Assert
            await _contactRepository.Received(1).AddAsync(Arg.Any<Contact>(), CancellationToken.None);

            result.IsSucc.Should().BeTrue();
            result.IfSucc(createContactDTO =>
            {
                createContactDTO.Id.Should().Be(expectedContact.Id);
                createContactDTO.FirstName.Should().Be(expectedContact.FirstName);
                createContactDTO.LastName.Should().Be(expectedContact.LastName);
                createContactDTO.UserId.Should().Be(expectedContact.UserId);
            });
        }

        [Fact]
        public async Task UpdateContactAsync_NoContactUpdated_ReturnUpdateEntityExceptionAsync()
        {
            // Arange
            var updateContactDTO = _fixture.Create<UpdateContactDTO>();
            _contactRepository.UpdateAsync(Arg.Any<Contact>(), CancellationToken.None)
                .ReturnsNullForAnyArgs();

            // Act
            var result = await _contactService.UpdateContactAsync(updateContactDTO, CancellationToken.None);

            // Assert
            await _contactRepository.Received(1).UpdateAsync(Arg.Any<Contact>(), CancellationToken.None);

            result.IsFail.Should().BeTrue();
            result.IfFail(error =>
            {
                error.IsExceptional.Should().BeTrue();
                error.ToException().Should().BeOfType<UpdateEntityException<Contact>>();
            });
        }

        [Fact]
        public async Task UpdateContactAsync_ContactUpdated_ReturnContactAsync()
        {
            // Arange
            var id = _fixture.Create<Guid>();

            var updateContactDTO = _fixture.Create<UpdateContactDTO>();
            var contact = _fixture.Build<Contact>()
                .With(c => c.Id, id)
                .With(c => c.FirstName, updateContactDTO.FirstName)
                .With(c => c.LastName, updateContactDTO.LastName)
                .Create();

            _contactRepository
                .UpdateAsync(Arg.Is<Contact>(c => c.FirstName == contact.FirstName), CancellationToken.None)
                .Returns(contact);

            var expectedContact = _fixture.Build<CreatedContactDTO>()
                .With(c => c.Id, id)
                .With(c => c.FirstName, contact.FirstName)
                .With(c => c.LastName, contact.LastName)
                .Create();
            // Act

            var result = await _contactService.UpdateContactAsync(updateContactDTO, CancellationToken.None);

            // Assert
            await _contactRepository.Received(1).UpdateAsync(Arg.Any<Contact>(), CancellationToken.None);

            result.IsSucc.Should().BeTrue();
            result.IfSucc(updatedContactDTO =>
            {
                updatedContactDTO.Id.Should().Be(expectedContact.Id);
                updatedContactDTO.FirstName.Should().Be(expectedContact.FirstName);
                updatedContactDTO.LastName.Should().Be(expectedContact.LastName);
            });
        }
    }
}
