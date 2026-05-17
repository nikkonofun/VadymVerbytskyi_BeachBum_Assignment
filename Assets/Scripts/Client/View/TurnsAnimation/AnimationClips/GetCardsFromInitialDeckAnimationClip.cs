using Client.View.ElementsView;
using Cysharp.Threading.Tasks;
using Shared.SharedModel.Data;

namespace Client.View.TurnsAnimation.AnimationClips
{
  public class GetCardsFromInitialDeckAnimationClip : IAnimationClip
  {
    public async UniTask Animate(GameView gameView, TurnEventData turnEventData, AnimationConfig animationConfig)
    {
      var initialDeck = gameView.InitialDeckView;
      var playerView = gameView.PlayersView[turnEventData.PlayerIdx];
      var cardsDeck = playerView.CardsDeck;
      var deckAnim = playerView.CardsDeckAnimation;
      
      AnimationClipUtil.SetTransformation(deckAnim, initialDeck);

      var cardsForPlayer = turnEventData.PlayerTurn.PlayDeckCount;
      deckAnim.Set(cardsForPlayer);
      initialDeck.Set(initialDeck.GetCardsCount() - cardsForPlayer);

      await AnimationClipUtil.MoveToAsync(deckAnim, cardsDeck, animationConfig.CardTransitionSec);
    }
  }
}