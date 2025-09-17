namespace BonefireCRM.Domain.Entities
{
    public class DealContactRole : BaseEntity
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public Guid RegisteredByUserId { get; set; }

        public User RegisteredByUser { get; set; } = null!;

        public virtual ICollection<DealContact> DealContacts { get; set; } = [];
    }
}