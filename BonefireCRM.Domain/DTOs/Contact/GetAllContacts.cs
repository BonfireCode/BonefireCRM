using BonefireCRM.SourceGenerator;

namespace BonefireCRM.Domain.DTOs.Contact
{
    [QueryExpressionsFor(typeof(Entities.Contact))]
    public class GetAllContacts
    {
        public Guid? Id { get; set; }
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
        public string? JobRole { get; set; } = string.Empty;
        public Guid? UserId { get; set; }
        public Guid? LifecycleStageId { get; set; }
        public Guid? CompanyId { get; set; }
        public string SortBy { get; set; } = string.Empty;
        public string SortDirection { get; set; } = "ASC";
    }
}
