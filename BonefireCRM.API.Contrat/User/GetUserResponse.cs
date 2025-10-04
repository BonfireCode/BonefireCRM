using BonefireCRM.API.Contrat.Contact;

namespace BonefireCRM.API.Contrat.User
{
    public sealed class GetUserResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public IEnumerable<GetContactResponse> Contacts { get; set; } = [];
        //public IEnumerable<GetActivityResponse> Activities { get; set; } = [];
    }
}
