namespace BonefireCRM.Domain.DTOs.Deal.Participant
{
    public class UpsertDealParticipantDTO
    {
        public Guid Id { get; set; }
        public Guid DealId { get; set; }
        public Guid ContactId { get; set; }
        public Guid DealParticipantRoleId { get; set; }
    }
}
