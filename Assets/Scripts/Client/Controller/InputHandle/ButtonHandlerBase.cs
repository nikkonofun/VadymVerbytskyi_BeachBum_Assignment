using System;
using Client.Controller.TurnsProcess;
using Client.Model;
using Client.Model.GameCommands;
using Client.Model.RequestResponseManagement;
using Client.View.Notification;
using Cysharp.Threading.Tasks;
using Shared.SharedModel.Dto;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Client.Controller.InputHandle
{
  public abstract class ButtonCommandHandlerBase : MonoBehaviour
  {
    [SerializeField] private Button _button;
    
    protected IAppModel AppModel { get; private set; }
    protected ITurnEventsFeed TurnEventsFeed;
    protected NotificationManager NotificationManager;
    private RequestResponseManager _requestResponseManager;
    
    private Guid? _nextRequestGuid;

    [Inject]
    private void Inject(RequestResponseManager requestResponseManager, IAppModel appModel, 
      ITurnEventsFeed turnEventsFeed, NotificationManager notificationManager)
    {
      _requestResponseManager = requestResponseManager;
      AppModel = appModel;
      TurnEventsFeed = turnEventsFeed;
      NotificationManager = notificationManager;
    }

    public void OnClick()
    {
      _nextRequestGuid ??= Guid.NewGuid();
      
      if (_requestResponseManager.RequestInProgress)
      {
        NotificationManager.Show("Another request is already in progress");
        return;
      }

      HandleClickAsync().Forget();
    }

    protected abstract IGameCommand GetCommand(Guid processingRequestGuid);
    
    protected void ShowNotificationError(IFaultDto faultDto) =>
      NotificationManager.Show(faultDto?.FailureCode?.ToString() ?? "Unknown error");

    private async UniTaskVoid HandleClickAsync()
    {
      ActivateButton(false);
      var command = GetCommand(_nextRequestGuid!.Value);
      await _requestResponseManager.TryExecuteCommandAsync(command, _nextRequestGuid!.Value);
      _nextRequestGuid = null;
      ActivateButton(true);
    }
    
    private void ActivateButton(bool isActive) =>
      _button.interactable = isActive; 
  }
}