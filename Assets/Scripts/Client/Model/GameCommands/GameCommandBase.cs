using System;
using Newtonsoft.Json;
using Shared.SharedModel.Dto;

namespace Client.Model.GameCommands
{
  public abstract class GameCommandBase<TRequest, TResponse>
    where TRequest : RequestDtoBase
    where TResponse : ResponseDtoBase
  {
    public abstract string MethodName { get; }

    // Callbacks for Controller
    private Action<TResponse> _onOk;
    private Action _onError;
    
    public TRequest RequestDto { get; private set; }
    
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
    
    public GameCommandBase<TRequest, TResponse> SetOnError(Action onError)
    {
      _onError = onError;
      return this;
    }

    public void ProcessResponse(TResponse response)
    {
      if (response == null) // TODO: make error codes
      {
        _onError?.Invoke();
        return;
      }

      ProcessResponseImpl(response);
      _onOk?.Invoke(response);
    }

    protected abstract void ProcessResponseImpl(TResponse response);
  }
}