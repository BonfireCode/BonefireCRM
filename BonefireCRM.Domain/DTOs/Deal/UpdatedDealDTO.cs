
using BonefireCRM.Domain.DTOs.Deal.Participant;

namespace BonefireCRM.Domain.DTOs.Deal
{
    public class UpdatedDealDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime ExpectedCloseDate { get; set; }
        public Guid PipelineStageId { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? PrimaryContactId { get; set; }
        public Guid UserId { get; set; }
        public IEnumerable<UpdatedDealParticipantDTO> DealParticipants { get; set; } = [];
    }
}
