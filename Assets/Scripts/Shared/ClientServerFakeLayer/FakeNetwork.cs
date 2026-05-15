using Cysharp.Threading.Tasks;
using Server.ServerModel;

namespace Shared.ClientServerFakeLayer
{
  public static class FakeNetwork
  {
    public static async UniTask<string> CallServerMethod(string methodName, string requestJson)
    {
      // TODO: make delays configurable and request failurable
      await UniTask.Delay(200);
      return ServerMain.CallMethod(methodName, requestJson);
    }
  }
}