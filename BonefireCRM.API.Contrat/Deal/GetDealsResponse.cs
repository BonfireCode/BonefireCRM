namespace BonefireCRM.API.Contrat.Deal
{
    public sealed class GetDealsResponse
    {
        public required IEnumerable<DealSummary> Deals { get; set; }


    }
}
