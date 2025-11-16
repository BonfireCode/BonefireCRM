using System.ComponentModel;

namespace BonefireCRM.API.Contrat.DealParticipantRole
{
    public sealed class GetDealParticipantRolesRequest
    {
        [Description("The unique identifier of the deal participant role.")]
        public Guid? Id { get; set; }

        [Description("The name of the deal participant role.")]
        public string? Name { get; set; } = string.Empty;

        [Description("The description of the deal participant role.")]
        public string? Description { get; set; } = string.Empty;

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
