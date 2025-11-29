namespace BonefireCRM.Domain.DTOs.Deal
{
    public class AssignedDealParticipantDTO
    {
        public Guid DealId { get; set; }
        public Guid ContactId { get; set; }
        public Guid DealParticipantRoleId { get; set; }
    }
}
