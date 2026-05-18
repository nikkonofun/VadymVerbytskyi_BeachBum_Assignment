using System.Collections.Generic;
using Client.View.ElementsView;
using Cysharp.Threading.Tasks;
using Shared.SharedModel.Data;

namespace Client.View.TurnsAnimation.AnimationClips
{
  public class TakeUnrevealedForWarAnimationClip : IAnimationClip
  {
    public async UniTask Animate(GameView gameView, TurnEventData turnEventData, AnimationConfig animationConfig)
    {
      var playerView = gameView.PlayersView[turnEventData.PlayerIdx];
      var deck = playerView.CardsDeck;
      var revealed = playerView.RevealedCard;
      var war = playerView.WarPile;
      var deckAnim = playerView.CardsDeckAnimation;
      var deckAnimExtra = playerView.CardsDeckAnimationExtra;
      var revealedAnim = playerView.RevealedCardAnimation;
      
      var tweenTasks = new List<UniTask>();
      
      revealedAnim.SetSprite(revealed.GetSprite());
      deckAnimExtra.Set(revealed.GetSprite() == null ? 0 : 1);
      revealed.SetSprite(null);
      AnimationClipUtil.SetTransformation(revealedAnim, revealed, customRotationZ: 0f);
      AnimationClipUtil.SetTransformation(deckAnimExtra, revealed, 0f, 0f);
      tweenTasks.Add(AnimationClipUtil.MoveToAsync(revealedAnim, war, animationConfig.CardTransitionSec));
      tweenTasks.Add(AnimationClipUtil.MoveToAsync(deckAnimExtra, war, animationConfig.CardTransitionSec));
      tweenTasks.Add(AnimationClipUtil.FlipHideAsync(revealedAnim, animationConfig.CardFlipSec)
        .ContinueWith(() => AnimationClipUtil.FlipShowAsync(deckAnimExtra, animationConfig.CardFlipSec)));
      
      deckAnim.Set(1);
      deck.Set(deck.GetCardsCount() - 1);
      AnimationClipUtil.SetTransformation(deckAnim, deck, customRotationZ: 0f);
      tweenTasks.Add(AnimationClipUtil.MoveToAsync(deckAnim, war, animationConfig.CardTransitionSec));

      await UniTask.WhenAll(tweenTasks);
    }
  }
}