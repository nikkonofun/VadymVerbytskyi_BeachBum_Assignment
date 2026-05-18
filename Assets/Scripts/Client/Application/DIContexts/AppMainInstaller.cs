using Client.Controller.TurnsProcess;
using Client.Model;
using Client.Model.RequestResponseManagement;
using Client.View.Configs;
using Client.View.Notification;
using Shared.ClientServerFakeLayer;
using UnityEngine;
using Zenject;

namespace Client.Application.DIContexts
{
  public class AppMainInstaller : MonoInstaller
  {
    [SerializeField] private NotificationManager _notificationManager;
    [SerializeField] private FakeNetworkConfigSo _fakeNetworkConfig;
    [SerializeField] private RetryPolicyConfigSo _retryPolicy;
    
    public override void InstallBindings()
    {
      Container.Bind<IAppModel>().To<AppModel>().AsSingle();
      Container.Bind<RequestResponseManager>().AsSingle();
      Container.Bind<ITurnEventsFeed>().To<TurnEventsFeed>().AsSingle();
      
      Container.Bind<FakeNetwork>().AsSingle();
      Container.Bind<FakeNetworkConfig>().FromInstance(
        new FakeNetworkConfig(_fakeNetworkConfig.MinDelayMs, _fakeNetworkConfig.MaxDelayMs,
          _fakeNetworkConfig.NetworkFailureChance, _fakeNetworkConfig.RequestLossChance,
          _fakeNetworkConfig.TimeoutMs)).AsSingle();
      
      Container.Bind<RetryPolicyConfig>().FromInstance(
        new RetryPolicyConfig(_retryPolicy.AttemptDelays, _retryPolicy.TimeoutSec));
      
      Container.Bind<NotificationManager>().FromComponentInNewPrefab(_notificationManager).AsSingle().NonLazy();
    }
  }
}