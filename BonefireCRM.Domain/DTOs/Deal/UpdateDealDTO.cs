using BonefireCRM.Domain.DTOs.Deal.Participant;

namespace BonefireCRM.Domain.DTOs.Deal
{
    public class UpdateDealDTO : DealSummaryDTO
    {
        public IEnumerable<UpsertDealParticipantDTO> DealParticipants { get; set; } = [];
    }
}
