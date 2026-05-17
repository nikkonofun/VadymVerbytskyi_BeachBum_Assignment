using System.Collections.Generic;
using Client.View.ElementsView;
using Cysharp.Threading.Tasks;
using Shared.SharedModel.Data;

namespace Client.View.TurnsAnimation.AnimationClips
{
  public class PutToSidePileAnimationClip : IAnimationClip
  {
    public async UniTask Animate(GameView gameView, TurnEventData turnEventData, AnimationConfig animationConfig)
    {
      var winnerSidePile = gameView.PlayersView[turnEventData.PlayerIdx].SidePile;

      var tweenTasks = new List<UniTask>();
      foreach (var playerView in gameView.PlayersView)
      {
        var revealedAnim = playerView.RevealedCardAnimation;
        var revealed = playerView.RevealedCard;
        var deckAnimExtra = playerView.CardsDeckAnimationExtra;
        deckAnimExtra.Set(turnEventData.PlayerTurn.SidePileCount);
        revealedAnim.SetSprite(revealed);
        revealed.Set(null);
        AnimationClipUtil.SetTransformation(revealedAnim, revealed);
        AnimationClipUtil.SetTransformation(deckAnimExtra, revealed, customScaleX: 0f);
        tweenTasks.Add(AnimationClipUtil.MoveToAsync(revealedAnim, winnerSidePile, animationConfig.CardTransitionSec));
        tweenTasks.Add(AnimationClipUtil.RotateToAsync(revealedAnim, winnerSidePile, animationConfig.CardRotationSec));
        tweenTasks.Add(AnimationClipUtil.FlipHideAsync(revealedAnim, animationConfig.CardFlipSec)
          .ContinueWith(() => AnimationClipUtil.FlipShowAsync(deckAnimExtra, animationConfig.CardFlipSec)));
        tweenTasks.Add(AnimationClipUtil.MoveToAsync(deckAnimExtra, winnerSidePile, animationConfig.CardTransitionSec));
        tweenTasks.Add(AnimationClipUtil.RotateToAsync(deckAnimExtra, winnerSidePile, animationConfig.CardRotationSec));

        var deckAnim = playerView.CardsDeckAnimation;
        var warPile = playerView.WarPile;
        deckAnim.Set(warPile.GetCardsCount());
        warPile.Set(0);
        AnimationClipUtil.SetTransformation(deckAnim, warPile);
        tweenTasks.Add(AnimationClipUtil.MoveToAsync(deckAnim, winnerSidePile, animationConfig.CardTransitionSec));
        tweenTasks.Add(AnimationClipUtil.RotateToAsync(deckAnim, winnerSidePile, animationConfig.CardRotationSec));
      }
      
      await UniTask.WhenAll(tweenTasks);
    }
  }
}