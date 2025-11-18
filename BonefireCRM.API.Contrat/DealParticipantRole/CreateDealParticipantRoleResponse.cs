using System.ComponentModel;

namespace BonefireCRM.API.Contrat.DealParticipantRole
{
    public sealed class CreateDealParticipantRoleResponse
    {
        [Description("The unique identifier of the created deal participant role.")]
        public Guid Id { get; set; }

        [Description("The name of the created deal participant role.")]
        public string Name { get; set; } = string.Empty;

        [Description("The description of the created deal participant role.")]
        public string Description { get; set; } = string.Empty;
    }
}
