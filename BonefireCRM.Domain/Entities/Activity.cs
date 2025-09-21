namespace BonefireCRM.Domain.Entities
{
    /// <summary>
    /// Abstract base class for activities (calls, meetings, tasks, emails).
    /// </summary>
    public abstract class Activity : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public Guid? ContactId { get; set; }
        public Contact? Contact { get; set; }

        public Guid? CompanyId { get; set; }
        public Company? Company { get; set; }

        public Guid? DealId { get; set; }
        public Deal? Deal { get; set; }
    }
}
