namespace Shared.SharedModel.Data
{
  public enum TurnEventKind
  {
    GetCardsFromInitialDeck,
    
    RevealTopFromDeck,
    PutToSidePile,
    TakeUnrevealedForWar,
    
    UseSidePileAsDeck,
  }
}