namespace BonefireCRM.Domain.DTOs.Deal.Participant
{
    public class AssignDealParticipantDTO
    {
        public Guid ContactId { get; set; }
        public Guid DealParticipantRoleId { get; set; }
    }
}
