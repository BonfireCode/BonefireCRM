using BonefireCRM.Domain.DTOs;

namespace BonefireCRM.Domain.Entities
{

    /// <summary>
    /// Represents a contact (an individual).
    /// </summary>
    public class Contact : BaseEntity
    {
        public string FullName { get; set; } = null!;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Role { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid LifecycleStageId { get; set; }

        public LifecycleStage LifecycleStage { get; set; } = null!;

        public Guid? CompanyId { get; set; } // Nullable in B2C
        public Company? Company { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public ICollection<DealContact> DealContacts { get; set; } = new List<DealContact>();
    }
}