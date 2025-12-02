namespace BonefireCRM.Domain.DTOs.Deal.Participant
{
    public class GetDealParticipantDTO
    {
        public Guid Id { get; set; }
        public Guid ContactId { get; set; }
        public Guid DealParticipantRoleId { get; set; }
    }
}
