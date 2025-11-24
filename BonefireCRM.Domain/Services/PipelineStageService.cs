using BonefireCRM.Domain.DTOs.PipelineStage;
using BonefireCRM.Domain.Entities;
using BonefireCRM.Domain.Infrastructure.Persistance;
using BonefireCRM.Domain.Mappers;
using BonefireCRM.SourceGenerator;
using LanguageExt;

namespace BonefireCRM.Domain.Services
{
    public class PipelineStageService
    {
        private readonly IBaseRepository<PipelineStage> _pipelineStageRepository;

        public PipelineStageService(IBaseRepository<PipelineStage> pipelineStageRepository)
        {
            _pipelineStageRepository = pipelineStageRepository;
        }

        public IEnumerable<GetPipelineStageDTO> GetAllPipelineStages(GetAllPipelineStagesDTO getAllPipelineStagesDTO, CancellationToken ct)
        {
            var filterExpression = PipelineStageQueryExpressions.Filter(getAllPipelineStagesDTO);

            var sortExpression = PipelineStageQueryExpressions.Sort(getAllPipelineStagesDTO.SortBy);

            var skip = (getAllPipelineStagesDTO.PageNumber - 1) * getAllPipelineStagesDTO.PageSize;
            var take = getAllPipelineStagesDTO.PageSize;

            var pipelineStages = _pipelineStageRepository.GetAll(filterExpression, sortExpression, getAllPipelineStagesDTO.SortDirection, skip, take, ct);

            var getPipelineStagesResultDTO = pipelineStages.Select(c => c.MapToGetDto());

            return getPipelineStagesResultDTO;
        }

        public async Task<Option<GetPipelineStageDTO>> GetPipelineStageAsync(Guid id, CancellationToken ct)
        {
            var pipelineStage = await _pipelineStageRepository.GetByIdAsync(id, ct);
            if (pipelineStage is null)
            {
                return Option<GetPipelineStageDTO>.None;
            }

            return pipelineStage.MapToGetDto();
        }
    }
}
