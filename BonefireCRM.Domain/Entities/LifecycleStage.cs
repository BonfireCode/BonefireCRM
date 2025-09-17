namespace BonefireCRM.Domain.Entities
{
    public class LifecycleStage : BaseEntity
    {
        public string Name { get; set; } = null!;

        // Navigation
        public ICollection<Contact> Contacts { get; set; } = new List<Contact>();
    }
}