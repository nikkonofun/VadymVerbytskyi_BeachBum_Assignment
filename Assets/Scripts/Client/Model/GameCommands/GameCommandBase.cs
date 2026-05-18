using System;
using Newtonsoft.Json;
using Shared.SharedModel.Dto;

namespace Client.Model.GameCommands
{
  public abstract class GameCommandBase<TRequest, TResponse> : IGameCommand
    where TRequest : RequestDtoBase
    where TResponse : ResponseDtoBase
  {
    public abstract string MethodName { get; }

    // Callbacks for Controller
    private Action<TResponse> _onOk;
    private Action<IFaultDto> _onError;
    
    public RequestDtoBase RequestDto { get; private set; }
    
    protected IAppModel AppModel;

    protected GameCommandBase(TRequest request, IAppModel appModel)
    {
      RequestDto = request;
      AppModel = appModel;
    }
    
    public ResponseDtoBase DeserializeResponse(string response) => 
      JsonConvert.DeserializeObject<TResponse>(response);

    public GameCommandBase<TRequest, TResponse> SetOnOk(Action<TResponse> onOk)
    {
      _onOk = onOk;
      return this;
    }
    
    public GameCommandBase<TRequest, TResponse> SetOnError(Action<IFaultDto> onError)
    {
      _onError = onError;
      return this;
    }

    public void ProcessResponse(ResponseDtoBase response)
    {
      var responseTyped = response as TResponse;
      if (responseTyped == null || responseTyped.FailureCode != null)
      {
        _onError?.Invoke(response);
        return;
      }

      ProcessResponseImpl(responseTyped);
      _onOk?.Invoke(responseTyped);
    }

    protected abstract void ProcessResponseImpl(TResponse response);
  }
}