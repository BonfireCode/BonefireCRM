namespace BonefireCRM.Domain.Entities
{

    /// <summary>
    /// Represents an individual contact, which may or may not be linked to a company (B2B vs B2C).
    /// </summary>
    public class Contact : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty; 

        public string PhoneNumber { get; set; } = string.Empty;

        public string JobRole { get; set; } = string.Empty;

        public Guid LifecycleStageId { get; set; }

        /// <summary>
        /// Nullable in B2C
        /// </summary>
        public Guid? CompanyId { get; set; }

        public Guid UserId { get; set; }

        public IEnumerable<DealParticipant> DealParticipants { get; set; } = [];
    }
}