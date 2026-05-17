using System;
using Client.Controller.TurnsProcess;
using Client.Model;
using Client.Model.GameCommands;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Client.Controller.InputHandle
{
  public abstract class ButtonCommandHandlerBase : MonoBehaviour
  {
    protected IAppModel AppModel { get; private set; }
    protected ITurnEventsFeed TurnEventsFeed;
    
    private RequestResponseManager _requestResponseManager;
    private Guid? _nextRequestGuid;

    [Inject]
    private void Inject(RequestResponseManager requestResponseManager, IAppModel appModel, ITurnEventsFeed turnEventsFeed)
    {
      _requestResponseManager = requestResponseManager;
      AppModel = appModel;
      TurnEventsFeed = turnEventsFeed;
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
      await _requestResponseManager.TryExecuteRequestAsync(command, _nextRequestGuid!.Value);
      _nextRequestGuid = null;
    }
  }
}