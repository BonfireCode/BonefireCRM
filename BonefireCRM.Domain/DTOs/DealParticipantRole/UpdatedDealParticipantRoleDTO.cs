namespace BonefireCRM.Domain.DTOs.DealParticipantRole
{
    public class UpdatedDealParticipantRoleDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Guid RegisteredByUserId { get; set; }
    }
}
