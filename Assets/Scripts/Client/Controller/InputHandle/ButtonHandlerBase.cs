using System;
using Client.Model;
using Client.Model.GameCommands;
using Cysharp.Threading.Tasks;
using Shared.SharedModel.Dto;
using UnityEngine;
using Zenject;

namespace Client.Controller.InputHandle
{
  public abstract class ButtonCommandHandlerBase : MonoBehaviour
  {
    protected IAppModel AppModel { get; private set; }
    
    private RequestResponseManager _requestResponseManager;
    private Guid? _nextRequestGuid;

    [Inject]
    public void Inject(RequestResponseManager requestResponseManager, IAppModel appModel)
    {
      _requestResponseManager = requestResponseManager;
      AppModel = appModel;
    }

    public void OnClick()
    {
      _nextRequestGuid ??= Guid.NewGuid();
      
      if (_requestResponseManager.RequestInProgress)
      {
        // TODO: show notification
        return;
      }

      HandleClickAsync().Forget();
    }

    protected abstract IGameCommand GetCommand(Guid processingRequestGuid);

    private async UniTaskVoid HandleClickAsync()
    {
      var command = GetCommand(_nextRequestGuid!.Value);
      await _requestResponseManager.TryExecuteRequest(command, _nextRequestGuid!.Value);
      _nextRequestGuid = null;
    }
  }
}