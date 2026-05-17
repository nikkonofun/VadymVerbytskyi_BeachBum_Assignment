using System;
using System.Threading.Tasks;
using Client.Model.GameCommands;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Shared.ClientServerFakeLayer;
using Shared.SharedModel.Dto;

namespace Client.Model
{
  public class RequestResponseManager
  {
    // TODO: implement retries
    // TODO: implement error handling
    
    private Guid? _processingRequestGuid;
    private IGameCommand _processingCommand;  // TODO: use with retries

    public bool RequestInProgress => _processingRequestGuid != null;

    public async UniTask<ResponseDtoBase> TryExecuteRequestAsync(IGameCommand command, Guid processingRequestGuid)
    {
      if (RequestInProgress || command == null)
        return null;

      _processingRequestGuid = processingRequestGuid;
      _processingCommand = command;

      try
      {
        var requestJson = JsonConvert.SerializeObject(command.RequestDto);
        
        var responseJson = await FakeNetwork.CallServerMethodAsync(command.MethodName, requestJson);
        if (string.IsNullOrEmpty(responseJson))
          return null;
        
        var response = command.DeserializeResponse(responseJson);
        command.ProcessResponse(response);
        return response;
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