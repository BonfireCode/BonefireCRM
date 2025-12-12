using BonefireCRM.Domain.DTOs.Deal.Participant;

namespace BonefireCRM.Domain.DTOs.Deal
{
    public class CreatedDealDTO : DealSummaryDTO
    {
        public IEnumerable<CreatedDealParticipantDTO> DealParticipants { get; set; } = [];
    }
}
