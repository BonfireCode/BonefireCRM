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

        public DealService(IBaseRepository<Deal> dealRepository)
        {
            _dealRepository = dealRepository;
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

            var isDeleted = await _dealRepository.DeleteAsync(id, ct);

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
    }
}
