namespace BonefireCRM.Domain.Entities
{
    /// <summary>
    /// Defines a role for a contact in the context of a deal (e.g., Decision Maker, Influencer).
    /// Stored in the database for flexibility rather than being an enum.
    /// </summary>
    public class DealContactRole : BaseEntity
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public Guid RegisteredByUserId { get; set; }

        public User RegisteredByUser { get; set; } = null!;

        public virtual ICollection<DealContact> DealContacts { get; set; } = [];
    }
}