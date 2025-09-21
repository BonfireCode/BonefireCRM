using BonefireCRM.Domain.Enums;

namespace BonefireCRM.Domain.Entities
{
    /// <summary>
    /// Represents a sales opportunity or deal.
    /// A deal can belong to a company (B2B) or just a contact (B2C).
    /// </summary>
    public class Deal : BaseEntity
    {
        public string Title { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime ExpectedCloseDate { get; set; }

        public Guid StageId { get; set; }
        public PipelineStage Stage { get; set; } = null!;

        public Guid? CompanyId { get; set; }
        public Company? Company { get; set; }

        public Guid? PrimaryContactId { get; set; }
        public Contact? PrimaryContact { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public ICollection<DealContact> DealContacts { get; set; } = new List<DealContact>();
    }
}