using BonefireCRM.Domain.Enums;

namespace BonefireCRM.Domain.Entities
{

    /// <summary>
    /// Represents an interaction with a contact or related to a deal.
    /// </summary>
    public class Interaction : BaseEntity
    {
        public InteractionType Type { get; set; }
        public DateTime InteractionDate { get; set; } = DateTime.UtcNow;
        public string Summary { get; set; } = string.Empty;
        public string? Notes { get; set; }

        public Guid LoggedByUserId { get; set; }
        public virtual User LoggedByUser { get; set; } = null!;

        public Guid? ContactId { get; set; }
        public virtual Contact? Contact { get; set; }

        public Guid? DealId { get; set; }
        public virtual Deal? Deal { get; set; }
    }
}