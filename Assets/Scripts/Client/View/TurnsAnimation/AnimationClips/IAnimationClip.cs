using Client.View.ElementsView;
using Cysharp.Threading.Tasks;
using Shared.SharedModel.Data;

namespace Client.View.TurnsAnimation.AnimationClips
{
  public interface IAnimationClip
  {
    public UniTask Animate(GameView gameView, TurnEventData turnEventData);
  }
}
