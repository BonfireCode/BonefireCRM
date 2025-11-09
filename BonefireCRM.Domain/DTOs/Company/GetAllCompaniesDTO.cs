using BonefireCRM.SourceGenerator;

namespace BonefireCRM.Domain.DTOs.Company
{
    [QueryExpressionsFor(typeof(Entities.Company))]
    public class GetAllCompaniesDTO
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Industry { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
        public string SortBy { get; set; } = string.Empty;
        public string SortDirection { get; set; } = string.Empty;
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}