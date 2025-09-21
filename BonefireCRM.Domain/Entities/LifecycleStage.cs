namespace BonefireCRM.Domain.Entities
{
    /// <summary>
    /// Defines the lifecycle stage of a contact (e.g., Lead, Customer, Prospect).
    /// </summary>
    public class LifecycleStage : BaseEntity
    {
        public string Name { get; set; } = null!;

        // Navigation
        public ICollection<Contact> Contacts { get; set; } = new List<Contact>();
    }
}