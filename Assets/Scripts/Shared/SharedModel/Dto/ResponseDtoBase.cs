using System.Collections.Generic;
using Newtonsoft.Json;
using Shared.SharedModel.Data;

namespace Shared.SharedModel.Dto
{
  public abstract class ResponseDtoBase
  {
    [JsonProperty("e")] public Queue<TurnEventData> TurnEvents;
    [JsonProperty("f")] public MatchResultData MatchResult;
  }
}