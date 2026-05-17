using System.Collections.Generic;
using Client.View.ElementsView;
using Cysharp.Threading.Tasks;
using Shared.SharedModel.Data;

namespace Client.View.TurnsAnimation.AnimationClips
{
  public class RevealTopFromDeckAnimationClip : IAnimationClip
  {
    public async UniTask Animate(GameView gameView, TurnEventData turnEventData, AnimationConfig animationConfig)
    {
      var playerView = gameView.PlayersView[turnEventData.PlayerIdx];
      var cardsDeck = playerView.CardsDeck;
      var deckAnim = playerView.CardsDeckAnimation;
      var revealedCardAnim = playerView.RevealedCardAnimation;
      var playerRevealedCard = playerView.RevealedCard;
      
      AnimationClipUtil.SetTransformation(deckAnim, cardsDeck, customRotationZ: 0f);
      AnimationClipUtil.SetTransformation(revealedCardAnim, cardsDeck, customScaleX: 0f);
      
      revealedCardAnim.Set(turnEventData.PlayerTurn.RevealedCard);
      deckAnim.Set(1);
      cardsDeck.Set(cardsDeck.GetCardsCount() - 1);

      var tweenTasks = new List<UniTask>();
      tweenTasks.Add(AnimationClipUtil.MoveToAsync(deckAnim, playerRevealedCard, animationConfig.CardTransitionSec));
      tweenTasks.Add(AnimationClipUtil.MoveToAsync(revealedCardAnim, playerRevealedCard, animationConfig.CardTransitionSec));

      tweenTasks.Add(AnimationClipUtil.FlipHideAsync(deckAnim, animationConfig.CardFlipSec)
        .ContinueWith(() => AnimationClipUtil.FlipShowAsync(revealedCardAnim, animationConfig.CardFlipSec)));
      
      await UniTask.WhenAll(tweenTasks);
    }
  }
}