using System;
using Client.Model.GameCommands;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Shared.ClientServerFakeLayer;

namespace Client.Model
{
  public class RequestResponseManager
  {
    // TODO: implement retries
    
    private Guid? _processingRequestGuid;
    private IGameCommand _processingCommand;  // TODO: use with retries

    public bool RequestInProgress => _processingRequestGuid != null;

    public async UniTask TryExecuteRequestAsync(IGameCommand command, Guid processingRequestGuid)
    {
      if (RequestInProgress || command == null)
        return;

      _processingRequestGuid = processingRequestGuid;
      _processingCommand = command;

      try
      {
        var requestJson = JsonConvert.SerializeObject(command.RequestDto);

        var responseJson = await FakeNetwork.CallServerMethodAsync(command.MethodName, requestJson);
        if (string.IsNullOrEmpty(responseJson))
          return;

        var response = command.DeserializeResponse(responseJson);
        command.ProcessResponse(response);
      }
      catch
      {
        command.ProcessResponse(null);
      }
      finally
      {
        CleanData();
      }
    }

    private void CleanData()
    {
      _processingRequestGuid = null;
      _processingCommand = null;
    }
  }
}