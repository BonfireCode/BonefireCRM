using BonefireCRM.Domain.DTOs.Deal;
using BonefireCRM.Domain.Entities;
using BonefireCRM.Domain.Exceptions;
using BonefireCRM.Domain.Infrastructure.Persistance;
using BonefireCRM.Domain.Mappers;
using BonefireCRM.SourceGenerator;
using LanguageExt;

namespace BonefireCRM.Domain.Services
{
    public class DealService
    {
        private readonly IBaseRepository<Deal> _dealRepository;
        private readonly IDealParticipantRepository _dealParticipantRepository;

        public DealService(IBaseRepository<Deal> dealRepository, IDealParticipantRepository dealParticipantRepository)
        {
            _dealRepository = dealRepository;
            _dealParticipantRepository = dealParticipantRepository;
        }

        public IEnumerable<GetDealDTO> GetAllDeals(GetAllDealsDTO getAllDealsDTO, CancellationToken ct)
        {
            var filterExpression = DealQueryExpressions.Filter(getAllDealsDTO);

            var sortExpression = DealQueryExpressions.Sort(getAllDealsDTO.SortBy);

            var skip = (getAllDealsDTO.PageNumber - 1) * getAllDealsDTO.PageSize;
            var take = getAllDealsDTO.PageSize;

            var deals = _dealRepository.GetAll(filterExpression, sortExpression, getAllDealsDTO.SortDirection, skip, take, ct);

            var getDealsResultDTO = deals.Select(c => c.MapToGetDto());

            return getDealsResultDTO;
        }

        public async Task<Option<GetDealDTO>> GetDealAsync(Guid id, CancellationToken ct)
        {
            var deal = await _dealRepository.GetByIdAsync(id, ct);
            if (deal is null)
            {
                return Option<GetDealDTO>.None;
            }

            return deal.MapToGetDto();
        }

        public async Task<bool> DeleteDealAsync(Guid id, CancellationToken ct)
        {
            //Domain validations if needed

            var deal = new Deal { Id = id };

            var isDeleted = await _dealRepository.DeleteAsync(deal, ct);

            return isDeleted;
        }

        public async Task<Fin<CreatedDealDTO>> CreateDealAsync(CreateDealDTO createDealDTO, CancellationToken ct)
        {
            //Domain validations if needed

            var deal = createDealDTO.MapToDeal();

            var createdDeal = await _dealRepository.AddAsync(deal, ct);
            if (createdDeal is null)
            {
                return Fin<CreatedDealDTO>.Fail(new AddEntityException<Deal>());
            }

            return createdDeal.MapToCreatedDto();
        }

        public async Task<Fin<UpdatedDealDTO>> UpdateDealAsync(UpdateDealDTO updateDealDTO, CancellationToken ct)
        {
            //Domain validations if needed

            var deal = updateDealDTO.MapToDeal();

            var updatedDeal = await _dealRepository.UpdateAsync(deal, ct);
            if (updatedDeal is null)
            {
                return Fin<UpdatedDealDTO>.Fail(new UpdateEntityException<Deal>());
            }

            return updatedDeal.MapToUpdatedDto();
        }

        public IEnumerable<GetDealParticipantDTO> GetAllDealParticipants(GetAllDealParticipantsDTO getDealParticipantsDTO, CancellationToken ct)
        {
            var skip = (getDealParticipantsDTO.PageNumber - 1) * getDealParticipantsDTO.PageSize;
            var take = getDealParticipantsDTO.PageSize;

            var dealParticipants = _dealParticipantRepository
                .GetAll(dealPartictipant => dealPartictipant.DealId == getDealParticipantsDTO.DealId,
                dealPartictipant => dealPartictipant.DealId == getDealParticipantsDTO.DealId,
                getDealParticipantsDTO.SortDirection, skip, take, ct);

            var getDealsResultDTO = dealParticipants.Select(c => c.MapToGetDto());

            return getDealsResultDTO;
        }

        public async Task<bool> RemoveDealParticipantAsync(Guid dealId, Guid dealParticipantId, CancellationToken ct)
        {
            //Domain validations if needed

            var dealParticipant = await _dealParticipantRepository.GetDealParticipantAsync(dealId, dealParticipantId, ct);

            if (dealParticipant == null)
            {
                return false;
            }

            var isDeleted = await _dealParticipantRepository.DeleteAsync(dealParticipant, ct);

            return isDeleted;
        }

        public async Task<Fin<AssignedDealParticipantDTO>> AssignDealParticipantAsync(AssignDealParticipantDTO assignDealParticipantDTO, CancellationToken ct)
        {
            //Domain validations if needed

            var dealParticipant = assignDealParticipantDTO.MapToDealParticipant();

            var assignedDealParticipant = await _dealParticipantRepository.AddAsync(dealParticipant, ct);
            if (assignedDealParticipant is null)
            {
                return Fin<AssignedDealParticipantDTO>.Fail(new AddEntityException<DealParticipant>());
            }

            return assignedDealParticipant.MapToAssignedDto();
        }
    }
}
