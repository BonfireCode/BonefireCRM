using System.ComponentModel;

namespace BonefireCRM.API.Contrat.Meeting
{
    public sealed class UpdateMeetingRequest
    {
        [Description("The unique identifier of the contact associated with the meeting.")]
        public Guid ContactId { get; set; }

        [Description("Optional. The unique identifier of the company linked to the meeting.")]
        public Guid? CompanyId { get; set; }

        [Description("Optional. The unique identifier of the deal linked to the meeting.")]
        public Guid? DealId { get; set; }

        [Description("The start date and time of the meeting.")]
        public DateTime StartTime { get; set; }

        [Description("The end date and time of the meeting.")]
        public DateTime EndTime { get; set; }

        [Description("The subject or title of the meeting.")]
        public string Subject { get; set; } = string.Empty;

        [Description("Additional notes or comments about the meeting.")]
        public string Notes { get; set; } = string.Empty;
    }
}
