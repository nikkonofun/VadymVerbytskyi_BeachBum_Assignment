using System.Collections.Generic;
using Client.View.ElementsView;
using Cysharp.Threading.Tasks;
using Shared.SharedModel.Data;

namespace Client.View.TurnsAnimation.AnimationClips
{
  public class UseSidePileAsDeckAnimationClip : IAnimationClip
  {
    public async UniTask Animate(GameView gameView, TurnEventData turnEventData, AnimationConfig animationConfig)
    {
      var playerView = gameView.PlayersView[turnEventData.PlayerIdx];
      var deck = playerView.CardsDeck;
      var sidePile = playerView.SidePile;
      var sidePileAnim = playerView.CardsDeckAnimation;
      
      sidePileAnim.Set(sidePile.GetCardsCount());
      deck.Set(0);
      sidePile.Set(0);
      AnimationClipUtil.SetTransformation(sidePileAnim, sidePile);
      
      var tweenTasks = new List<UniTask>();
      tweenTasks.Add(AnimationClipUtil.MoveToAsync(sidePileAnim, deck, animationConfig.CardTransitionSec));
      tweenTasks.Add(AnimationClipUtil.RotateToAsync(sidePileAnim, 0f, animationConfig.CardRotationSec));
      
      await UniTask.WhenAll(tweenTasks);
    }
  }
}