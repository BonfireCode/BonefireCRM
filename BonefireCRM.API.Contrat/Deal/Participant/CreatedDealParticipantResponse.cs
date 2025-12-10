using System.ComponentModel;

namespace BonefireCRM.API.Contrat.Deal.Participant
{
    public sealed class CreatedDealParticipantResponse
    {
        [Description("The unique identifier of the deal participant.")]
        public Guid Id { get; set; }

        [Description("The unique identifier of the contact assigned to the deal.")]
        public Guid ContactId { get; set; }

        [Description("The unique identifier of the participant role assigned within the deal.")]
        public Guid DealParticipantRoleId { get; set; }
    }
}
