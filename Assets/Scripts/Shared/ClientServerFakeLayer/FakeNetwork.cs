using System;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Server.ServerModel;
using Shared.SharedModel.Dto;
using Zenject;

namespace Shared.ClientServerFakeLayer
{
  public class FakeNetwork
  {
    private FakeNetworkConfig _config;
    private Random _random = new Random();
    
    [Inject]
    public void Inject(FakeNetworkConfig config) =>
      _config = config;
    
    public async UniTask<string> CallServerMethodAsync(string methodName, string requestJson)
    {
      await UniTask.Delay(_random.Next(_config.MinDelayMs, _config.MaxDelayMs));
      
      if (_random.NextDouble() < _config.NetworkFailureChance) 
        return JsonConvert.SerializeObject(new ResponseDtoBase { Failure = FailureCode.NetworkError });

      if (_random.NextDouble() < _config.RequestLossChance)
      {
        await UniTask.Delay(_config.TimeoutMs);
        return JsonConvert.SerializeObject(new ResponseDtoBase { Failure = FailureCode.Timeout });
      }
      
      return ServerMain.CallMethod(methodName, requestJson);
    }
  }
}