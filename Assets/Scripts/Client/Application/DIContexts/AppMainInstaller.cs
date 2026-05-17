using Client.Model;
using Zenject;

namespace Client.Application
{
  public class AppMainInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      Container.Bind<IAppModel>().To<AppModel>().AsSingle();
      Container.Bind<RequestResponseManager>().AsSingle();
    }
  }
}