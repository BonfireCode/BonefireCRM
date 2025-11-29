using System.ComponentModel;

namespace BonefireCRM.API.Contrat.Deal
{
    public sealed class GetDealParticipantsRequest
    {
        [Description("The page number.")]
        public int? PageNumber { get; set; }

        [Description("The page size.")]
        public int? PageSize { get; set; }
    }
}
