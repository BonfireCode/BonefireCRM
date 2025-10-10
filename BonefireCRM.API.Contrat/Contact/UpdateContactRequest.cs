namespace BonefireCRM.API.Contrat.Contact
{
    public sealed class UpdateContactRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string JobRole { get; set; } = string.Empty;
        public Guid LifecycleStageId { get; set; }
        public Guid? CompanyId { get; set; }
    }
}
