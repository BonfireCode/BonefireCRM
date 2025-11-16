using BonefireCRM.Domain.DTOs.DealParticipantRole;
using BonefireCRM.Domain.Entities;
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

        public async Task<Option<GetDealParticipantRoleDTO>> GetDealParticipantRoleAsync(Guid id, Guid registeredByUserId,  CancellationToken ct)
        {
            var dealParticipantRole = await _dealParticipantRoleRepository.GetAsync(a => a.Id == id && a.RegisteredByUserId == registeredByUserId, ct);

            if (dealParticipantRole is null)
            {
                return Option<GetDealParticipantRoleDTO>.None;
            }

            return dealParticipantRole.MapToGetDto();
        }
    }
}
