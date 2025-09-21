namespace BonefireCRM.Domain.Entities
{
    /// <summary>
    /// Many-to-many link between deals and contacts, with a specific role.
    /// </summary>
    public class DealContact : BaseEntity
    {
        public Guid DealId { get; set; }
        public Deal Deal { get; set; } = null!;

        public Guid ContactId { get; set; }
        public Contact Contact { get; set; } = null!;

        public Guid DealContactRoleId { get; set; }
        public DealContactRole Role { get; set; } = null!;
    }
}
