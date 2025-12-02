namespace BonefireCRM.Domain.DTOs.Deal.Participant
{
    public class UpdateDealParticipantDTO
    {
        public Guid Id { get; set; }
        public Guid DealId { get; set; }
        public Guid ContactId { get; set; }
        public Guid DealParticipantRoleId { get; set; }
    }
}
