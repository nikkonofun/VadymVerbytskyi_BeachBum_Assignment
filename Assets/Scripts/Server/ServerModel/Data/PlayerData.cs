using Shared.SharedModel.Data;

namespace Server.ServerModel.Data
{
  public class PlayerData
  {
    public CardsDeck PlayDeck { get; } = new();
    public CardsDeck SidePile { get; } = new();

    public CardsDeck HiddenOnTable { get; } = new();
    public CardData? RevealedOnTable { get; set; }
  }
}