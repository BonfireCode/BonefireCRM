using System.ComponentModel;

namespace BonefireCRM.API.Contrat.Company
{
    public sealed class CreateCompanyResponse
    {
        [Description("The unique identifier of the created company.")]
        public Guid Id { get; set; }

        [Description("The name of the company.")]
        public string Name { get; set; } = string.Empty;

        [Description("The industry sector in which the company operates.")]
        public string Industry { get; set; } = string.Empty;

        [Description("The physical address of the company.")]
        public string Address { get; set; } = string.Empty;

        [Description("The primary phone number of the company.")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
