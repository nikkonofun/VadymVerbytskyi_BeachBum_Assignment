using System;
using System.Collections.Generic;
using Client.View.ElementsView;
using Client.View.TurnsAnimation.AnimationClips;
using Cysharp.Threading.Tasks;
using Shared.SharedModel.Data;
using UnityEngine;

namespace Client.View.TurnsAnimation
{
  public class TurnsAnimator : MonoBehaviour, ITurnsAnimator
  {
    [SerializeField] private GameView _gameView;

    private Dictionary<TurnEventKind, IAnimationClip> _eventToClips = new()
    {
      { TurnEventKind.GetCardsFromInitialDeck, new GetCardsFromInitialDeckAnimationClip() },
      { TurnEventKind.RevealTopFromDeck, new RevealTopFromDeckAnimationClip() },
      { TurnEventKind.PutToSidePile, new PutToSidePileAnimationClip() },
      { TurnEventKind.TakeUnrevealedForWar, new TakeUnrevealedForWarAnimationClip() },
      { TurnEventKind.UseSidePileAsDeck, new UseSidePileAsDeckAnimationClip() },
    };
    
    public void Animate(TurnEventData turnData, Action onAnimationFinished)
    {
      AnimateImpl(turnData, onAnimationFinished).Forget();
    }

    private async UniTaskVoid AnimateImpl(TurnEventData turnData, Action onAnimationFinished)
    {
      await _eventToClips[turnData.EventKind].Animate(_gameView, turnData);
      EnsureProperValues(turnData);
      onAnimationFinished();
    }

    private void EnsureProperValues(TurnEventData turnData)
    {
      var playerView = _gameView.PlayersView[turnData.PlayerIdx];
      var playerTurn = turnData.PlayerTurn;
      playerView.CardsDeck.CardsCount.text = playerTurn.PlayDeckCount.ToString();
      playerView.SidePile.CardsCount.text = playerTurn.SidePileCount.ToString();
      playerView.WarPile.CardsCount.text = playerTurn.HiddenCount.ToString();
      
      if (playerTurn.RevealedCard.HasValue)
        playerView.RevealedCard.Set(playerTurn.RevealedCard.Value);
    }
  }
}