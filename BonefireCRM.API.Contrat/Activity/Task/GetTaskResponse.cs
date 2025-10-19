using System.ComponentModel;

namespace BonefireCRM.API.Contrat.Task
{
    public sealed class GetTaskResponse
    {
        [Description("The unique identifier of the retrieved task.")]
        public Guid Id { get; set; }

        [Description("The unique identifier of the contact associated with the task.")]
        public Guid ContactId { get; set; }

        [Description("Optional. The unique identifier of the company linked to the task.")]
        public Guid? CompanyId { get; set; }

        [Description("Optional. The unique identifier of the deal linked to the task.")]
        public Guid? DealId { get; set; }

        [Description("The subject or title of the task.")]
        public string Subject { get; set; } = string.Empty;

        [Description("A detailed description of the task.")]
        public string Description { get; set; } = string.Empty;

        [Description("The due date and time for completing the task.")]
        public DateTime DueDate { get; set; }

        [Description("Indicates whether the task has been completed.")]
        public bool IsCompleted { get; set; }
    }
}
