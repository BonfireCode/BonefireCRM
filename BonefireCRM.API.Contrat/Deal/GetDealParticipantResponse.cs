using System.ComponentModel;

namespace BonefireCRM.API.Contrat.Deal
{
    public sealed class GetDealParticipantResponse
    {
        [Description("The unique identifier of the deal.")]
        public Guid DealId { get; set; }

        [Description("The unique identifier of the contact participating in the deal.")]
        public Guid ContactId { get; set; }

        [Description("The unique identifier of the participant's role within the deal.")]
        public Guid DealParticipantRoleId { get; set; }
    }
}
