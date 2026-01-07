using System.ComponentModel;

namespace BonefireCRM.API.Contrat.Meeting
{
    public sealed class GetMeetingsRequest
    {
        [Description("The unique identifier of the meeting.")]
        public Guid? Id { get; set; }

        [Description("The subject of the meeting.")]
        public string? Subject { get; set; }

        [Description("The notes of the meeting.")]
        public string? Notes { get; set; }

        [Description("The unique identifier of the contact associated with the meeting.")]
        public Guid? ContactId { get; set; }

        [Description("The unique identifier of the user who scheduled the meeting.")]
        public Guid? UserId { get; set; }

        [Description("The unique identifier of the company associated with the meeting.")]
        public Guid? CompanyId { get; set; }

        [Description("The unique identifier of the deal associated with the meeting.")]
        public Guid? DealId { get; set; }

        [Description("The start date and time of the meeting.")]
        public DateTime? StartTime { get; set; }

        [Description("The end date and time of the meeting.")]
        public DateTime? EndTime { get; set; }

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