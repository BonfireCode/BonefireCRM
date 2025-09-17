namespace BonefireCRM.Domain.Entities
{
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
