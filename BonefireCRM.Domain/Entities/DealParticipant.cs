namespace BonefireCRM.Domain.Entities
{
    /// <summary>
    /// Many-to-many link between deals and contacts, with a specific role.
    /// </summary>
    public class DealParticipant : BaseEntity
    {
        public Guid DealId { get; set; }

        public Guid ContactId { get; set; }

        public Guid DealParticipantRoleId { get; set; }
    }
}
