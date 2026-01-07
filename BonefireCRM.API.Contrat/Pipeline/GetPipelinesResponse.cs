namespace BonefireCRM.API.Contrat.Pipeline
{
    public sealed class GetPipelinesResponse
    {
        public IEnumerable<GetPipelineSumaryResponse> Pipelines { get; set; } = [];
    }
}
