using BonefireCRM.Domain.DTOs.Pipeline;
using BonefireCRM.Domain.Infrastructure.Persistance;
using BonefireCRM.Domain.Mappers;
using BonefireCRM.SourceGenerator;
using LanguageExt;

namespace BonefireCRM.Domain.Services
{
    public class PipelineService
    {
        private readonly IPipelineRepository _pipelineRepository;

        public PipelineService(IPipelineRepository pipelineRepository)
        {
            _pipelineRepository = pipelineRepository;
        }

        public GetPipelinesDTO GetAllPipelines(GetAllPipelinesDTO getAllPipelinesDTO, CancellationToken ct)
        {
            var filterExpression = PipelineQueryExpressions.Filter(getAllPipelinesDTO);

            var sortExpression = PipelineQueryExpressions.Sort(getAllPipelinesDTO.SortBy);

            var skip = (getAllPipelinesDTO.PageNumber - 1) * getAllPipelinesDTO.PageSize;
            var take = getAllPipelinesDTO.PageSize;

            var pipelines = _pipelineRepository.GetAll(filterExpression, sortExpression, getAllPipelinesDTO.SortDirection, skip, take, ct);

            return pipelines.MapToGetDto();
        }

        public async Task<Option<GetPipelineDTO>> GetPipelineAsync(Guid id, CancellationToken ct)
        {
            var pipeline = await _pipelineRepository.GetPipelineIncludeStagesAsync(id, ct);
            if (pipeline is null)
            {
                return Option<GetPipelineDTO>.None;
            }

            return pipeline.MapToGetDto();
        }
    }
}
