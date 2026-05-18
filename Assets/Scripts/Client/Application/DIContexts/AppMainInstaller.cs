using Client.Controller.TurnsProcess;
using Client.Model;
using Client.View.Notification;
using UnityEngine;
using Zenject;

namespace Client.Application.DIContexts
{
  public class AppMainInstaller : MonoInstaller
  {
    [SerializeField] private NotificationManager _notificationManager;
    
    public override void InstallBindings()
    {
      Container.Bind<IAppModel>().To<AppModel>().AsSingle();
      Container.Bind<RequestResponseManager>().AsSingle();
      Container.Bind<ITurnEventsFeed>().To<TurnEventsFeed>().AsSingle();
      
      Container.Bind<NotificationManager>().FromComponentInNewPrefab(_notificationManager).AsSingle().NonLazy();
    }
  }
}