using BonefireCRM.Domain.DTOs.LifeCycleStage;
using BonefireCRM.Domain.Entities;
using BonefireCRM.Domain.Infrastructure.Persistance;
using BonefireCRM.Domain.Mappers;
using BonefireCRM.SourceGenerator;
using LanguageExt;

namespace BonefireCRM.Domain.Services
{
    public class LifecycleStageService
    {
        private readonly IBaseRepository<LifecycleStage> _lifecycleStageRepository;

        public LifecycleStageService(IBaseRepository<LifecycleStage> lifecycleStageRepository)
        {
            _lifecycleStageRepository = lifecycleStageRepository;
        }

        public IEnumerable<GetLifecycleStageDTO> GetAllLifecycleStages(GetAllLifecycleStagesDTO getAllLifecycleStagesDTO, CancellationToken ct)
        {
            var filterExpression = LifecycleStageQueryExpressions.Filter(getAllLifecycleStagesDTO);

            var sortExpression = LifecycleStageQueryExpressions.Sort(getAllLifecycleStagesDTO.SortBy);

            var skip = (getAllLifecycleStagesDTO.PageNumber - 1) * getAllLifecycleStagesDTO.PageSize;
            var take = getAllLifecycleStagesDTO.PageSize;

            var pipelineLifeCycleStages = _lifecycleStageRepository.GetAll(filterExpression, sortExpression, getAllLifecycleStagesDTO.SortDirection, skip, take, ct);

            var getLifecycleStagesResultDTO = pipelineLifeCycleStages.Select(c => c.MapToGetDto());

            return getLifecycleStagesResultDTO;
        }

        public async Task<Option<GetLifecycleStageDTO>> GetLifecycleStageAsync(Guid id, CancellationToken ct)
        {
            var pipelineLifeCycleStage = await _lifecycleStageRepository.GetAsync(id, ct);
            if (pipelineLifeCycleStage is null)
            {
                return Option<GetLifecycleStageDTO>.None;
            }

            return pipelineLifeCycleStage.MapToGetDto();
        }
    }
}
