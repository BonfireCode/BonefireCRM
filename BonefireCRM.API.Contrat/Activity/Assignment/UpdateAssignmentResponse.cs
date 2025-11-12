using System.ComponentModel;

namespace BonefireCRM.API.Contrat.Activity.Assignment
{
    public sealed class UpdateAssignmentResponse
    {
        [Description("The unique identifier of the updated assignment.")]
        public Guid Id { get; set; }

        [Description("The unique identifier of the contact associated with the assignment.")]
        public Guid ContactId { get; set; }

        [Description("Optional. The unique identifier of the company linked to the assignment.")]
        public Guid? CompanyId { get; set; }

        [Description("Optional. The unique identifier of the deal linked to the assignment.")]
        public Guid? DealId { get; set; }

        [Description("The subject or title of the assignment.")]
        public string Subject { get; set; } = string.Empty;

        [Description("A detailed description of the assignment.")]
        public string Description { get; set; } = string.Empty;

        [Description("The due date and time for completing the assignment.")]
        public DateTime DueDate { get; set; }

        [Description("Indicates whether the assignment has been completed.")]
        public bool IsCompleted { get; set; }
    }
}
