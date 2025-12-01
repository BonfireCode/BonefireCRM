using System.ComponentModel;

namespace BonefireCRM.API.Contrat.Deal
{
    public sealed class GetDealsRequest
    {
        [Description("Optional. Filters results by the unique identifier of the deal.")]
        public Guid? Id { get; set; }

        [Description("Filters results by the title or name of the deal.")]
        public string? Title { get; set; } = string.Empty;

        [Description("Filters results by the monetary value of the deal.")]
        public decimal? Amount { get; set; }

        [Description("Filters results by the expected close date of the deal.")]
        public DateTime? ExpectedCloseDate { get; set; }

        [Description("Filters results by the unique identifier of the pipeline stage.")]
        public Guid? PipelineStageId { get; set; }

        [Description("Used in B2B deals. Filters results by the unique identifier of the company associated with the deal.")]
        public Guid? CompanyId { get; set; }

        [Description("Used in B2C deals. Filters results by the unique identifier of the primary contact associated with the deal.")]
        public Guid? PrimaryContactId { get; set; }

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
