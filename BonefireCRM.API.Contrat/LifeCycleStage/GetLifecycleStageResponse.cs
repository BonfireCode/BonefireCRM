using System.ComponentModel;

namespace BonefireCRM.API.Contrat.LifeCycleStage
{
    public sealed class GetLifecycleStageResponse
    {
        [Description("The unique identifier of the lifecycle stage.")]
        public Guid Id { get; set; }

        [Description("The name of the lifecycle stage.")]
        public string Name { get; set; } = string.Empty;
    }
}
