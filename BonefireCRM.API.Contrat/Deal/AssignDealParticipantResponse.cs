using System.ComponentModel;

namespace BonefireCRM.API.Contrat.Deal
{
    public sealed class AssignDealParticipantResponse
    {
        [Description("The unique identifier of the deal.")]
        public Guid DealId { get; set; }

        [Description("The unique identifier of the contact assigned to the deal.")]
        public Guid ContactId { get; set; }

        [Description("The unique identifier of the participant role assigned within the deal.")]
        public Guid DealParticipantRoleId { get; set; }
    }
}
