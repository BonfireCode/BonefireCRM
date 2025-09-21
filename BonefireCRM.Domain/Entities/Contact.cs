namespace BonefireCRM.Domain.Entities
{

    /// <summary>
    /// Represents an individual contact, which may or may not be linked to a company (B2B vs B2C).
    /// </summary>
    public class Contact : BaseEntity
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Email { get; set; } 
        public string? PhoneNumber { get; set; }
        public string? JobRole { get; set; }
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