using Shared.SharedModel.Data;
using Shared.SharedModel.Dto.MakeTurn;

namespace Client.Model.Data
{
  public class PlayerData
  {
    public CardData? RevealedCard { get; private set; }
    public int HiddenCount { get; private set; }
    public int PlayDeckCount { get; private set; }
    public int SidePileCount { get; private set; }

    public void Update(PlayerTurnData turnData)
    {
      RevealedCard = turnData.RevealedCard;
      HiddenCount = turnData.HiddenCount;
      PlayDeckCount = turnData.PlayDeckCount;
      SidePileCount = turnData.SidePileCount;
    }
  }
}