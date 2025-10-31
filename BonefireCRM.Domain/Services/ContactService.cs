using BonefireCRM.Domain.DTOs.Contact;
using BonefireCRM.Domain.Entities;
using BonefireCRM.Domain.Exceptions;
using BonefireCRM.Domain.Infrastructure.Persistance;
using BonefireCRM.Domain.Mappers;
using BonefireCRM.SourceGenerator;
using LanguageExt;

namespace BonefireCRM.Domain.Services
{
    public class ContactService
    {
        private readonly IBaseRepository<Contact> _contactRepository;

        public ContactService(IBaseRepository<Contact> contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public IEnumerable<GetContactDTO> GetAllContacts(GetAllContactsDTO getAllContactsDTO, CancellationToken ct)
        {
            var filterExpression = ContactQueryExpressions.Filter(getAllContactsDTO);

            var sortExpression = ContactQueryExpressions.Sort(getAllContactsDTO.SortBy);

            int skip = (getAllContactsDTO.PageNumber - 1) * getAllContactsDTO.PageSize;
            int take = getAllContactsDTO.PageSize;

            var contacts = _contactRepository.GetAll(filterExpression, sortExpression, getAllContactsDTO.SortDirection, skip, take, ct);

            var getContactsResultDTO = contacts.Select(c => c.MapToGetDto());

            return getContactsResultDTO;
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
