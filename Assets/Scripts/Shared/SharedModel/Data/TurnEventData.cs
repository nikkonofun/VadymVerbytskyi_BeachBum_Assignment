using Newtonsoft.Json;

namespace Shared.SharedModel.Data
{
  public class TurnEventData
  {
    [JsonProperty("k")] public TurnEventKind EventKind;
    [JsonProperty("p")] public PlayerTurnData PlayerTurn;
    [JsonProperty("i")] public int PlayerIdx;
  }
  
  public class PlayerTurnData
  {
    [JsonProperty("r")] public CardData? RevealedCard;
    [JsonProperty("h")] public int HiddenCount;
    [JsonProperty("d")] public int PlayDeckCount;
    [JsonProperty("p")] public int SidePileCount;
  }
}