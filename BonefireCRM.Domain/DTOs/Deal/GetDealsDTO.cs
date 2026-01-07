namespace BonefireCRM.Domain.DTOs.Deal
{
    public class GetDealsDTO
    {
        public IEnumerable<DealSummaryDTO> Deals { get; set; } = [];
    }
}
