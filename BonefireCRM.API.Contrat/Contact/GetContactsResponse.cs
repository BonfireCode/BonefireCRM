using BonefireCRM.API.Contrat.Shared;

namespace BonefireCRM.API.Contrat.Contact
{
    public sealed class GetContactsRequest: PaginationFilterRequest
    {
        public Guid? Id { get; set; }
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
    }
}
