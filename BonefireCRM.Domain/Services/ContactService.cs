using BonefireCRM.Domain.DTOs.Contact;
using BonefireCRM.Domain.DTOs.Shared;
using BonefireCRM.Domain.Entities;
using BonefireCRM.Domain.Exceptions;
using BonefireCRM.Domain.Infrastructure.Persistance;
using BonefireCRM.Domain.Mappers;
using LanguageExt;
using System.Linq.Expressions;

namespace BonefireCRM.Domain.Services
{
    public class ContactService
    {
        private readonly IBaseRepository<Contact> _contactRepository;

        public ContactService(IBaseRepository<Contact> contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<Fin<PagedResultDTO<GetContactDTO>>> GetContactsAsync(GetContactsDTO getContactsDTO, CancellationToken ct)
        {
            if (getContactsDTO is null)
                return Fin<PagedResultDTO<GetContactDTO>>.Fail("Invalid request.");

            // Build filtering predicate
            Expression<Func<Contact, bool>> predicate = contact =>
                (getContactsDTO.Id == null || contact.Id == getContactsDTO.Id) &&
                (string.IsNullOrEmpty(getContactsDTO.FirstName) || contact.FirstName.Contains(getContactsDTO.FirstName)) &&
                (string.IsNullOrEmpty(getContactsDTO.LastName) || contact.LastName.Contains(getContactsDTO.LastName));

            // Handle sorting
            Func<IQueryable<Contact>, IOrderedQueryable<Contact>>? orderBy = null;

            if (!string.IsNullOrWhiteSpace(getContactsDTO.SortBy))
            {
                bool ascending = string.Equals(getContactsDTO.SortDirection, "asc", StringComparison.OrdinalIgnoreCase);
                orderBy = query => ascending
                    ? query.OrderByDynamic(getContactsDTO.SortBy)
                    : query.OrderByDescendingDynamic(getContactsDTO.SortBy);
            }

            // Calculate pagination
            int skip = (getContactsDTO.PageNumber - 1) * getContactsDTO.PageSize;
            int take = getContactsDTO.PageSize;

            // Fetch from repository
            var contacts = await _contactRepository.GetAllAsync(
                predicate: predicate,
                orderBy: orderBy,
                skip: skip,
                take: take,
                ct: ct
            );

            return contacts.MapToGetAllDto(getContactsDTO.PageNumber, getContactsDTO.PageSize);
        }

        public async Task<Option<GetContactDTO>> GetContactAsync(Guid id, CancellationToken ct)
        {
            var contact = await _contactRepository.GetAsync(id, ct);
            if (contact is null)
            {
                return Option<GetContactDTO>.None;
            }

            return contact.MapToGetDto();
        }

        public async Task<bool> DeleteContactAsync(Guid id, CancellationToken ct)
        {
            //Domain validations if needed

            var contact = new Contact { Id = id };

            var isDeleted = await _contactRepository.DeleteAsync(contact, ct);

            return isDeleted;
        }

        public async Task<Fin<CreatedContactDTO>> CreateContactAsync(CreateContactDTO createContactDTO, CancellationToken ct)
        {
            //Domain validations if needed

            var contact = createContactDTO.MapToContact();

            var createdContact = await _contactRepository.AddAsync(contact, ct);
            if (createdContact is null)
            {
                return Fin<CreatedContactDTO>.Fail(new AddEntityException<Contact>());
            }

            return createdContact.MapToCreatedDto();
        }

        public async Task<Fin<UpdatedContactDTO>> UpdateContactAsync(UpdateContactDTO updateContactDTO, CancellationToken ct)
        {
            //Domain validations if needed

            var contact = updateContactDTO.MapToContact();

            var updatedContact = await _contactRepository.UpdateAsync(contact, ct);
            if (updatedContact is null)
            {
                return Fin<UpdatedContactDTO>.Fail(new UpdateEntityException<Contact>());
            }

            return updatedContact.MapToUpdatedDto();
        }
    }
}
