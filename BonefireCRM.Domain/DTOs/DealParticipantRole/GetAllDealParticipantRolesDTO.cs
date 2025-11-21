using BonefireCRM.SourceGenerator;

namespace BonefireCRM.Domain.DTOs.DealParticipantRole
{
    [QueryExpressionsFor(typeof(Entities.DealParticipantRole))]
    public class GetAllDealParticipantRolesDTO
    {
        public Guid? Id { get; set; }

        public Guid? RegisteredByUserId { get; set; }

        public string? Name { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;

        public string SortBy { get; set; } = string.Empty;

        public string SortDirection { get; set; } = string.Empty;

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
