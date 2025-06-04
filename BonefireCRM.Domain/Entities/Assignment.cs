using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BonefireCRM.Domain.Entities
{
    public class Assignment : BaseEntity
    {
        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }

        [ForeignKey(nameof(User))]
        public Guid AssignedToId { get; set; }

        [ForeignKey(nameof(Contact))]
        public Guid? ContactId { get; set; } // Optional link to contact

        public User? AssignedTo { get; set; }
        public Contact? Contact { get; set; }
    }
}
