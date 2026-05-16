using Newtonsoft.Json;

namespace Shared.SharedModel.Dto.LaunchMatch
{
  public class LaunchMatchResponseDto : ResponseDtoBase
  {
    [JsonProperty("c")] public int[] PlayerCardsCount;
  }
}