using System.ComponentModel;

namespace BonefireCRM.API.Contrat.Contact
{
    public sealed class CreateContactRequest
    {
        [Description("The first name of the contact.")]
        public string FirstName { get; set; } = string.Empty;

        [Description("The last name of the contact.")]
        public string LastName { get; set; } = string.Empty;

        [Description("The email address of the contact.")]
        public string Email { get; set; } = string.Empty;

        [Description("The phone number of the contact.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Description("The job role or position of the contact.")]
        public string JobRole { get; set; } = string.Empty;

        [Description("The unique identifier of the lifecycle stage assigned to the contact.")]
        public Guid LifecycleStageId { get; set; }

        [Description("Optional. The unique identifier of the company associated with the contact.")]
        public Guid? CompanyId { get; set; }
    }
}
