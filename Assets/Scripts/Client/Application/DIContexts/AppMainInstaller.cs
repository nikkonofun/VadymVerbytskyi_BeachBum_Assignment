using Client.Controller.TurnsProcess;
using Client.Model;
using Zenject;

namespace Client.Application.DIContexts
{
  public class AppMainInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      Container.Bind<IAppModel>().To<AppModel>().AsSingle();
      Container.Bind<RequestResponseManager>().AsSingle();
      Container.Bind<ITurnEventsFeed>().To<TurnEventsFeed>().AsSingle();
    }
  }
}