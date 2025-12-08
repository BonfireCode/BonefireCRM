using BonefireCRM.Domain.DTOs.Deal.Participant;

namespace BonefireCRM.Domain.DTOs.Deal
{
    public class GetDealDTO : DealSummaryDTO
    {
        public IEnumerable<GetDealParticipantDTO> DealParticipants { get; set; } = [];
    }
}
