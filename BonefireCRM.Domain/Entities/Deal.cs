using BonefireCRM.Domain.Enums;

namespace BonefireCRM.Domain.Entities
{
    /// <summary>
    /// Represents a sales opportunity or deal.
    /// A deal can belong to a company (B2B) or just a contact (B2C).
    /// </summary>
    public class Deal : BaseEntity
    {
        public string Title { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public DateTime ExpectedCloseDate { get; set; }

        public Guid PipelineStageId { get; set; }

        /// <summary>
        /// Nullable in B2C
        /// </summary>
        public Guid? CompanyId { get; set; }

        /// <summary>
        /// Nullable in B2B
        /// </summary>
        public Guid? PrimaryContactId { get; set; }

        public Guid UserId { get; set; }

        public ICollection<DealParticipant> DealParticipants { get; set; } = [];
    }
}