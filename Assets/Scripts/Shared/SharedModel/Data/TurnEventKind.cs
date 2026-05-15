namespace Shared.SharedModel.Data
{
  public enum TurnEventKind
  {
    ProcessInitialDeck,
    
    RevealTopFromDeck,
    PutToSidePile,
    TakeUnrevealedForWar,
    
    UseSidePileAsDeck,
  }
}