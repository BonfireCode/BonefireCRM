using System.ComponentModel;

namespace BonefireCRM.API.Contrat.Company
{
    public sealed class UpdateCompanyRequest
    {
        [Description("The updated name of the company.")]
        public string Name { get; set; } = string.Empty;

        [Description("The updated industry sector in which the company operates.")]
        public string Industry { get; set; } = string.Empty;

        [Description("The updated physical address of the company.")]
        public string Address { get; set; } = string.Empty;

        [Description("The updated primary phone number of the company.")]
        public string PhoneNumber { get; set; } = string.Empty;
    }

}
