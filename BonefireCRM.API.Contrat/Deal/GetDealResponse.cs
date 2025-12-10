using BonefireCRM.API.Contrat.Deal.Participant;
using System.ComponentModel;

namespace BonefireCRM.API.Contrat.Deal
{
    public sealed class GetDealResponse : DealSummary
    {
        [Description("A list of participants assigned to the deal.")]
        public IEnumerable<GetDealParticipantResponse> DealParticipants { get; set; } = [];
    }
}
