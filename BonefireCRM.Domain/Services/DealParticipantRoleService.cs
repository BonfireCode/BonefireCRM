using BonefireCRM.Domain.DTOs.DealParticipantRole;
using BonefireCRM.Domain.Entities;
using BonefireCRM.Domain.Exceptions;
using BonefireCRM.Domain.Infrastructure.Persistance;
using BonefireCRM.Domain.Mappers;
using BonefireCRM.SourceGenerator;
using LanguageExt;

namespace BonefireCRM.Domain.Services
{
    public class DealParticipantRoleService
    {
        private readonly IBaseRepository<DealParticipantRole> _dealParticipantRoleRepository;

        public DealParticipantRoleService(IBaseRepository<DealParticipantRole> dealParticipantRoleRepository)
        {
            _dealParticipantRoleRepository = dealParticipantRoleRepository;
        }

        public IEnumerable<GetDealParticipantRoleDTO> GetAllParticipantRoles(GetAllDealParticipantRolesDTO getAllDealParticipantRolesDTO, CancellationToken ct)
        {
            var filterExpression = DealParticipantRoleQueryExpressions.Filter(getAllDealParticipantRolesDTO);

            var sortExpression = DealParticipantRoleQueryExpressions.Sort(getAllDealParticipantRolesDTO.SortBy);

            var skip = (getAllDealParticipantRolesDTO.PageNumber - 1) * getAllDealParticipantRolesDTO.PageSize;
            var take = getAllDealParticipantRolesDTO.PageSize;

            var dealParticipantRoles = _dealParticipantRoleRepository.GetAll(filterExpression, sortExpression, getAllDealParticipantRolesDTO.SortDirection, skip, take, ct);

            var getDealParticipantRolesResultDTO = dealParticipantRoles.Select(c => c.MapToGetDto());

            return getDealParticipantRolesResultDTO;
        }

        public async Task<Option<GetDealParticipantRoleDTO>> GetDealParticipantRoleAsync(Guid id, CancellationToken ct)
        {
            var dealParticipantRole = await _dealParticipantRoleRepository.GetByIdAsync(id, ct);

            if (dealParticipantRole is null)
            {
                return Option<GetDealParticipantRoleDTO>.None;
            }

            return dealParticipantRole.MapToGetDto();
        }

        public async Task<Fin<CreatedDealParticipantRoleDTO>> CreateDealParticipantRoleAsync(CreateDealParticipantRoleDTO createDealParticipantRoleDTO, CancellationToken ct)
        {
            //Domain validations if needed

            var dealParticipantRole = createDealParticipantRoleDTO.MapToDealParticipantRole();

            var createdDealParticipantRole = await _dealParticipantRoleRepository.AddAsync(dealParticipantRole, ct);
            if (createdDealParticipantRole is null)
            {
                return Fin<CreatedDealParticipantRoleDTO>.Fail(new AddEntityException<DealParticipantRole>());
            }

            return createdDealParticipantRole.MapToCreatedDto();
        }

        public async Task<Fin<UpdatedDealParticipantRoleDTO>> UpdateDealParticipantRoleAsync(UpdateDealParticipantRoleDTO updateDealParticipantRoleDTO, CancellationToken ct)
        {
            //Domain validations if needed

            var dealParticipantRole = updateDealParticipantRoleDTO.MapToDealParticipantRole();

            var updatedDealParticipantRole = await _dealParticipantRoleRepository.UpdateAsync(dealParticipantRole, ct);
            if (updatedDealParticipantRole is null)
            {
                return Fin<UpdatedDealParticipantRoleDTO>.Fail(new UpdateEntityException<DealParticipantRole>());
            }

            return updatedDealParticipantRole.MapToUpdatedDto();
        }

        public async Task<bool> DeleteDealParticipantRoleAsync(Guid id, CancellationToken ct)
        {
            //Domain validations if needed

            var dealParticipantRoleDTO = new DealParticipantRole { Id = id };

            var isDeleted = await _dealParticipantRoleRepository.DeleteAsync(dealParticipantRoleDTO, ct);

            return isDeleted;
        }
    }
}
