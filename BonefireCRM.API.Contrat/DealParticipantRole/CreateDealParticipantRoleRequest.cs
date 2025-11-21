using System.ComponentModel;

namespace BonefireCRM.API.Contrat.DealParticipantRole
{
    public sealed class CreateDealParticipantRoleRequest
    {
        [Description("The name of the deal participant role to create.")]
        public string Name { get; set; } = string.Empty;

        [Description("The description of the deal participant role to create.")]
        public string Description { get; set; } = string.Empty;
    }
}
