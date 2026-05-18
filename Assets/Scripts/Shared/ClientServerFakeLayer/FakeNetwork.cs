using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Server.ServerModel;
using Shared.SharedModel.Dto;
using UnityEngine;
using Zenject;

namespace Shared.ClientServerFakeLayer
{
  public class FakeNetwork
  {
    private FakeNetworkConfig _config;
    
    [Inject]
    public void Inject(FakeNetworkConfig config) =>
      _config = config;
    
    public async UniTask<string> CallServerMethodAsync(string methodName, string requestJson)
    {
      await UniTask.Delay(Random.Range(_config.MinDelayMs, _config.MaxDelayMs));
      
      if (Random.Range(0.0f, 1.0f) < _config.NetworkFailureChance) 
        return JsonConvert.SerializeObject(new ResponseDtoBase { Failure = FailureCode.NetworkError });

      if (Random.Range(0.0f, 1.0f) < _config.RequestLossChance)
      {
        await UniTask.Delay(_config.TimeoutMs);
        return JsonConvert.SerializeObject(new ResponseDtoBase { Failure = FailureCode.Timeout });
      }
      
      return ServerMain.CallMethod(methodName, requestJson);
    }
  }
}