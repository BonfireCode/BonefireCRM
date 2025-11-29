namespace BonefireCRM.Domain.DTOs.Deal
{
    public class GetAllDealParticipantsDTO
    {
        public Guid DealId { get; set; }
        public string SortDirection { get; set; } = string.Empty;
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
