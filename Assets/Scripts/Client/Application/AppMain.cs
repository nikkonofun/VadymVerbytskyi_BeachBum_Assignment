using Client.Model;
using Zenject;

namespace Client.Application
{
  public class AppMain : MonoInstaller
  {
    public override void InstallBindings()
    {
      Container.Bind<IAppModel>().To<AppModel>().AsSingle();
    }
  }
}