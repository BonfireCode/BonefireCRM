namespace BonefireCRM.Domain.DTOs.DealParticipantRole
{
    public class CreateDealParticipantRoleDTO
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Guid RegisteredByUserId { get; set; }
    }
}
