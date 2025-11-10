namespace BonefireCRM.API.Contrat.Shared
{
    public class Filter
    {
        public string Field { get; set; } = string.Empty;
        public string Operator { get; set; } = "equals";
        public string? Value { get; set; }
    }
}
