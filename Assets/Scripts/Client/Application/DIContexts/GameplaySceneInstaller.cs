using Client.Controller.TurnsProcess;
using Client.View.MatchEndAnimation;
using Client.View.TurnsAnimation;
using Zenject;

namespace Client.Application.DIContexts
{
  public class GameplaySceneInstaller : MonoInstaller
  {
    public override void InstallBindings()
    {
      Container.Bind<ITurnsAnimator>().To<TurnsAnimator>().FromComponentInHierarchy().AsSingle();
      Container.Bind<IMatchEndAnimator>().To<MatchEndAnimator>().FromComponentInHierarchy().AsSingle();
      
      Container.BindInterfacesAndSelfTo<TurnsProcessor>().AsSingle().NonLazy();
    }
  }
}