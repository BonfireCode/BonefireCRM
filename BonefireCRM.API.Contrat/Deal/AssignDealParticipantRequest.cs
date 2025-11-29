using System.ComponentModel;

namespace BonefireCRM.API.Contrat.Deal
{
    public sealed class AssignDealParticipantRequest
    {
        [Description("The unique identifier of the contact being assigned to the deal.")]
        public Guid ContactId { get; set; }

        [Description("The unique identifier of the role that the participant will have within the deal.")]
        public Guid DealParticipantRoleId { get; set; }
    }
}
