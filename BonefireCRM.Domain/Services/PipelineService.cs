using BonefireCRM.Domain.DTOs.Pipeline;
using BonefireCRM.Domain.Entities;
using BonefireCRM.Domain.Infrastructure.Persistance;
using BonefireCRM.Domain.Mappers;
using BonefireCRM.SourceGenerator;
using LanguageExt;

namespace BonefireCRM.Domain.Services
{
    public class PipelineService
    {
        private readonly IBaseRepository<Pipeline> _pipelineRepository;

        public PipelineService(IBaseRepository<Pipeline> pipelineRepository)
        {
            _pipelineRepository = pipelineRepository;
        }

        public IEnumerable<GetPipelineDTO> GetAllPipelines(GetAllPipelinesDTO getAllPipelinesDTO, CancellationToken ct)
        {
            var filterExpression = PipelineQueryExpressions.Filter(getAllPipelinesDTO);

            var sortExpression = PipelineQueryExpressions.Sort(getAllPipelinesDTO.SortBy);

            var skip = (getAllPipelinesDTO.PageNumber - 1) * getAllPipelinesDTO.PageSize;
            var take = getAllPipelinesDTO.PageSize;

            var pipelines = _pipelineRepository.GetAll(filterExpression, sortExpression, getAllPipelinesDTO.SortDirection, skip, take, ct);

            var getPipelinesResultDTO = pipelines.Select(c => c.MapToGetDto());

            return getPipelinesResultDTO;
        }

        public async Task<Option<GetPipelineDTO>> GetPipelineAsync(Guid id, CancellationToken ct)
        {
            var pipeline = await _pipelineRepository.GetByIdAsync(id, ct);
            if (pipeline is null)
            {
                return Option<GetPipelineDTO>.None;
            }

            return pipeline.MapToGetDto();
        }
    }
}
