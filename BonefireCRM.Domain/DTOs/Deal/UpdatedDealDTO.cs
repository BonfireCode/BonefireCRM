
using BonefireCRM.Domain.DTOs.Deal.Participant;

namespace BonefireCRM.Domain.DTOs.Deal
{
    public class UpdatedDealDTO : DealSummaryDTO
    {
        public IEnumerable<UpsertedDealParticipantDTO> DealParticipants { get; set; } = [];
    }
}
