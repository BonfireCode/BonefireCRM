using BonefireCRM.SourceGenerator;

namespace BonefireCRM.Domain.DTOs.User
{
    [QueryExpressionsFor(typeof(Entities.User))]
    public class GetAllUsersDTO
    {
        public Guid? Id { get; set; }
        public Guid? RegisterId { get; set; }
        public string? UserName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string SortBy { get; set; } = string.Empty;
        public string SortDirection { get; set; } = string.Empty;
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}