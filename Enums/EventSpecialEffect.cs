namespace Monopoly.Enums;

/// <summary>
/// Which event pool the event belongs.
/// </summary>
public enum EventSpecialEffect {
    // Value events
    GiveMoneyToBank,
    TakeMoneyFromBank,
    GiveMoneyToAllPlayers,
    TakeMoneyFromAllPlayers,
    PayForHousesAndHotels, // Hotels are always 4 times more expensive
    VisitPlace,
    
    // Non value events - value doesn't have any effect
    // e.g. {DrawCard, 2} - doesn't mean draw two cards, always one card will be drawn
    DrawCommunityChest,
    DrawChance,
    GoToJail
}