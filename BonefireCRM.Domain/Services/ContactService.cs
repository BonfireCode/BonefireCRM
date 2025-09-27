using BonefireCRM.Domain.DTOs.Contact;
using BonefireCRM.Domain.Entities;
using BonefireCRM.Domain.Exceptions;
using BonefireCRM.Domain.Infrastructure.Persistance;
using BonefireCRM.Domain.Mappers;
using LanguageExt;
using LanguageExt.Common;

namespace BonefireCRM.Domain.Services
{
    public class ContactService
    {
        private readonly IBaseRepository<Contact> _contactRepository;

        public ContactService(IBaseRepository<Contact> contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<Option<GetContactDTO>> GetContactAsync(Guid id, CancellationToken ct)
        {
            var contact = await _contactRepository.GetAsync(id, ct);
            if (contact is null)
            {
                return null;
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

            return Fin<CreatedContactDTO>.Succ(createdContact.MapToCreatedDto());
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

            return Fin<UpdatedContactDTO>.Succ(updatedContact.MapToUpdatedDto());
        }
    }
}
