using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BonefireCRM.Domain.Entities
{
    public class Assignment : BaseEntity
    {
        public string Title { get; set; } = string.Empty;

        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }

        public Guid AssignedToId { get; set; }

        public Guid? ContactId { get; set; }

        public User? AssignedTo { get; set; }
        public Contact? Contact { get; set; }
    }
}
