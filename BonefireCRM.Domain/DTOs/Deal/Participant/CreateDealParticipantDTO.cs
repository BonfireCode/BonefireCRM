namespace BonefireCRM.Domain.DTOs.Deal.Participant
{
    public class CreateDealParticipantDTO
    {
        public Guid ContactId { get; set; }
        public Guid DealParticipantRoleId { get; set; }
    }
}
