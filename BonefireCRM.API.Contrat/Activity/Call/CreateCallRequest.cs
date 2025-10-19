using System.ComponentModel;

namespace BonefireCRM.API.Contrat.Call
{
    public sealed class CreateCallRequest
    {
        [Description("The unique identifier of the contact associated with the call.")]
        public Guid ContactId { get; set; }

        [Description("Optional. The unique identifier of the company linked to the call.")]
        public Guid? CompanyId { get; set; }

        [Description("Optional. The unique identifier of the deal linked to the call.")]
        public Guid? DealId { get; set; }

        [Description("The date and time when the call occurred.")]
        public DateTime CallTime { get; set; }

        [Description("The duration of the call.")]
        public TimeSpan Duration { get; set; }

        [Description("Additional notes or comments about the call.")]
        public string Notes { get; set; } = string.Empty;
    }
}
