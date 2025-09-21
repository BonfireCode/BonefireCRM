namespace BonefireCRM.Domain.DTOs.Email
{
    public class EmailDTO
    {
        public string From { get; set; } = string.Empty;

        public List<string> To { get; set; } = [];

        public string FromName { get; set; } = string.Empty;

        public string Subject { get; set; } = string.Empty;

        public string Body { get; set; } = string.Empty;
    }
}
