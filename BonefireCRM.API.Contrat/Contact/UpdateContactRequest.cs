namespace BonefireCRM.API.Contrat.Contact
{
    using System;
    using System.ComponentModel;

    public sealed class UpdateContactRequest
    {
        [Description("The updated first name of the contact.")]
        public string FirstName { get; set; } = string.Empty;

        [Description("The updated last name of the contact.")]
        public string LastName { get; set; } = string.Empty;

        [Description("The updated email address of the contact.")]
        public string Email { get; set; } = string.Empty;

        [Description("The updated phone number of the contact.")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Description("The updated job role or position of the contact.")]
        public string JobRole { get; set; } = string.Empty;

        [Description("The updated lifecycle stage identifier assigned to the contact.")]
        public Guid LifecycleStageId { get; set; }

        [Description("Optional. The updated unique identifier of the company associated with the contact.")]
        public Guid? CompanyId { get; set; }
    }

}
