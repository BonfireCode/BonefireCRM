using System.ComponentModel;

namespace BonefireCRM.API.Contrat.Activity.Assignment
{
    public sealed class GetAssignmentsRequest
    {
        [Description("The unique identifier of the assignment.")]
        public Guid? Id { get; set; }

        [Description("The title of the assignment.")]
        public string? Title { get; set; }

        [Description("The description of the assignment.")]
        public string? Description { get; set; }

        [Description("The due date of the assignment.")]
        public DateTime? DueDate { get; set; }

        [Description("The status of the assignment.")]
        public string? Status { get; set; }

        [Description("The unique identifier of the contact associated with the assignment.")]
        public Guid? ContactId { get; set; }

        [Description("The unique identifier of the user who created the assignment.")]
        public Guid? UserId { get; set; }

        [Description("The unique identifier of the company associated with the assignment.")]
        public Guid? CompanyId { get; set; }

        [Description("The unique identifier of the deal associated with the assignment.")]
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