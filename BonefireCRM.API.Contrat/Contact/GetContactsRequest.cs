using System.ComponentModel;

namespace BonefireCRM.API.Contrat.Contact
{
    public sealed class GetContactsRequest
    {
        [Description("The unique identifier of the contact.")]
        public Guid? Id { get; set; }

        [Description("The first name of the contact.")]
        public string? FirstName { get; set; }

        [Description("The last name of the contact.")]
        public string? LastName { get; set; }

        [Description("The email address of the contact.")]
        public string? Email { get; set; }

        [Description("The phone number of the contact.")]
        public string? PhoneNumber { get; set; }

        [Description("The job role or position of the contact.")]
        public string? JobRole { get; set; }

        [Description("The unique identifier of the user associated with the contact.")]
        public Guid? UserId { get; set; }

        [Description("The unique identifier of the lifecycle stage assigned to the contact.")]
        public Guid? LifecycleStageId { get; set; }

        [Description("The unique identifier of the company associated with the contact.")]
        public Guid? CompanyId { get; set; }

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
