using BonefireCRM.API.Contrat.Deal.Participant;
using System.ComponentModel;

namespace BonefireCRM.API.Contrat.Deal
{
    public sealed class CreateDealRequest
    {
        [Description("The title or name of the deal.")]
        public string Title { get; set; } = string.Empty;

        [Description("The monetary value of the deal.")]
        public decimal Amount { get; set; }

        [Description("The expected date when the deal is anticipated to close.")]
        public DateTime ExpectedCloseDate { get; set; }

        [Description("The unique identifier of the pipeline stage assigned to the deal.")]
        public Guid PipelineStageId { get; set; }

        [Description("Used in B2B deals. The unique identifier of the company associated with the deal.")]
        public Guid? CompanyId { get; set; }

        [Description("Used in B2C deals. The unique identifier of the primary contact associated with the deal.")]
        public Guid? PrimaryContactId { get; set; }

        [Description("A list of participants assigned to the deal.")]
        public IEnumerable<CreateDealParticipantRequest> DealParticipants { get; set; } = [];
    }
}
