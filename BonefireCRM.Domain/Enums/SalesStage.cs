namespace BonefireCRM.Domain.Enums
{

    /// <summary>
    /// Defines the possible stages of a sales deal for the MVP.
    /// </summary>
    public enum SalesStage
    {
        Lead = 1,
        Qualified = 2,
        Proposal = 3,
        Negotiation = 4,
        ClosedWon = 5,
        ClosedLost = 6
    }
}