using Cysharp.Threading.Tasks;
using Server.ServerModel;
using UnityEngine;

namespace Shared.ClientServerFakeLayer
{
  public static class FakeNetwork
  {
    // TODO: make SO
    private const int MinDelayMs = 0;
    private const int MaxDelayMs = 0;
    private const float FailureChance = 0.0f; // 0..1 -> 0%..100%
    private const int FailureDelayMs = 5000;
    
    public static async UniTask<string> CallServerMethodAsync(string methodName, string requestJson)
    {
      if (Random.Range(0.0f, 1.0f) < FailureChance)
      {
        await UniTask.Delay(FailureDelayMs);
        return null;
      }

      await UniTask.Delay(Random.Range(MinDelayMs, MaxDelayMs));
      return ServerMain.CallMethod(methodName, requestJson);
    }
  }
}