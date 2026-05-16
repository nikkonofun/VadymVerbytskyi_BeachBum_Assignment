using System.Collections.Generic;
using Newtonsoft.Json;
using Shared.SharedModel.Data;

namespace Shared.SharedModel.Dto.MakeTurn
{
  public class MakeTurnResponseDto : ResponseDtoBase
  {
    [JsonProperty("e")] public Queue<TurnEventData> TurnEvents;
    [JsonProperty("f")] public MatchResultData MatchResult;
  }
  
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