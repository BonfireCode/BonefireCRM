using System.ComponentModel;

namespace BonefireCRM.API.Contrat.Company
{
    public sealed class GetCompaniesRequest
    {
        [Description("The unique identifier of the company.")]
        public Guid? Id { get; set; }

        [Description("The name of the company.")]
        public string? Name { get; set; }

        [Description("The industry of the company.")]
        public string? Industry { get; set; }

        [Description("The address of the company.")]
        public string? Address { get; set; }

        [Description("The phone number of the company.")]
        public string? PhoneNumber { get; set; }

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