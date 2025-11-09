using System.ComponentModel;

namespace BonefireCRM.API.Contrat.Task
{
    public sealed class GetTasksRequest
    {
        [Description("The unique identifier of the task.")]
        public Guid? Id { get; set; }

        [Description("The title of the task.")]
        public string? Title { get; set; }

        [Description("The description of the task.")]
        public string? Description { get; set; }

        [Description("The due date of the task.")]
        public DateTime? DueDate { get; set; }

        [Description("The status of the task.")]
        public string? Status { get; set; }

        [Description("The unique identifier of the contact associated with the task.")]
        public Guid? ContactId { get; set; }

        [Description("The unique identifier of the user who created the task.")]
        public Guid? UserId { get; set; }

        [Description("The unique identifier of the company associated with the task.")]
        public Guid? CompanyId { get; set; }

        [Description("The unique identifier of the deal associated with the task.")]
        public Guid? DealId { get; set; }

        [Description("The sort by property name.")]
        public string? SortBy { get; set; }

        [Description("The sort direction: asc or desc.")]
        public string? SortDirection { get; set; }

        [Description("The page number.")]
        public int? PageNumber { get; set; }

        [Description("The page size.")]
        public int? PageSize { get; set; }
    }
}