using System.ComponentModel;

namespace BonefireCRM.API.Contrat.User
{
    public sealed class GetUsersRequest
    {
        [Description("The unique identifier of the user.")]
        public Guid? Id { get; set; }

        [Description("The unique identifier of the user registration.")]
        public Guid? RegisterId { get; set; }

        [Description("The username of the user.")]
        public string? UserName { get; set; }

        [Description("The email address of the user.")]
        public string? Email { get; set; }

        [Description("The first name of the user.")]
        public string? FirstName { get; set; }

        [Description("The last name of the user.")]
        public string? LastName { get; set; }

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