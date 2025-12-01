namespace BonefireCRM.Domain.Entities
{
    /// <summary>
    /// Defines a role for a contact in the context of a deal (e.g., Decision Maker, Influencer).
    /// Stored in the database for flexibility rather than being an enum.
    /// </summary>
    public class DealParticipantRole : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Guid RegisteredByUserId { get; set; }

        public List<DealParticipant> DealParticipants { get; set; } = [];
    }
}