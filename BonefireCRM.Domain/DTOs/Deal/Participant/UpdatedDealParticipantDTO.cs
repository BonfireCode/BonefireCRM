namespace BonefireCRM.Domain.DTOs.Deal.Participant
{
    public class UpdatedDealParticipantDTO
    {
        public Guid Id { get; set; }
        public Guid ContactId { get; set; }
        public Guid DealParticipantRoleId { get; set; }
    }
}
