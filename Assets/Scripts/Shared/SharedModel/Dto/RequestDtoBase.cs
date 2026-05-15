using System;
using Newtonsoft.Json;

namespace Shared.SharedModel.Dto
{
  public abstract class RequestDtoBase
  {
    [JsonProperty("id")] public Guid RequestId;
  }
}