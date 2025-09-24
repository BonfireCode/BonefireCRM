namespace BonefireCRM.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }

        public DateTime DateCreated { get; private set; } = DateTime.UtcNow;

        public DateTime? LastModifiedDate { get; private set; }
    }
}