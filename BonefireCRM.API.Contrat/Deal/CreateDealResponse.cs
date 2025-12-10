using BonefireCRM.API.Contrat.Deal.Participant;
using System.ComponentModel;

namespace BonefireCRM.API.Contrat.Deal
{
    public sealed class CreateDealResponse : DealSummary
    {
        [Description("A list of participants assigned to the deal.")]
        public IEnumerable<CreatedDealParticipantResponse> DealParticipants { get; set; } = [];
    }
}
