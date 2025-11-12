using System.ComponentModel;

namespace BonefireCRM.API.Contrat.Call
{
    public sealed class GetCallsRequest
    {
        [Description("The unique identifier of the call.")]
        public Guid? Id { get; set; }

        [Description("The description of the call.")]
        public string? Description { get; set; }

        [Description("The date and time of the call.")]
        public DateTime? CallDateTime { get; set; }

        [Description("The duration of the call in minutes.")]
        public int? DurationMinutes { get; set; }

        [Description("The unique identifier of the contact associated with the call.")]
        public Guid? ContactId { get; set; }

        [Description("The unique identifier of the user who made the call.")]
        public Guid? UserId { get; set; }

        [Description("The unique identifier of the company associated with the call.")]
        public Guid? CompanyId { get; set; }

        [Description("The unique identifier of the deal associated with the call.")]
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