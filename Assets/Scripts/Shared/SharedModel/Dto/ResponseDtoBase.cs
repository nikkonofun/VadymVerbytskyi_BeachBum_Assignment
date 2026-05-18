using System.Collections.Generic;
using Newtonsoft.Json;
using Shared.SharedModel.Data;

namespace Shared.SharedModel.Dto
{
  public class ResponseDtoBase : IFaultDto
  {
    public FailureCode? FailureCode => Failure;
    
    [JsonProperty("e")] public Queue<TurnEventData> TurnEvents;
    [JsonProperty("f")] public MatchResultData MatchResult;

    [JsonProperty("fc")] public FailureCode? Failure;
  }
  
  public interface IFaultDto
  {
    FailureCode? FailureCode { get; }
  }

  public enum FailureCode
  {
    NetworkError,
    Timeout
  }
}