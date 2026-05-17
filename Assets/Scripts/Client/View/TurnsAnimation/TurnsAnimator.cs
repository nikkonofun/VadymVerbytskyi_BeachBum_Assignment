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
    [SerializeField] private AnimationConfig _animationConfig;

    private readonly Dictionary<TurnEventKind, IAnimationClip> _eventToClips = new()
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

    private void Awake()
    {
      Prepare();
    }

    private async UniTaskVoid AnimateImpl(TurnEventData turnData, Action onAnimationFinished)
    {
      await _eventToClips[turnData.EventKind].Animate(_gameView, turnData, _animationConfig);
      AnimationClipUtil.ResetAnimationElements(_gameView);
      UpdateValues(turnData);
      onAnimationFinished();
    }

    private void UpdateValues(TurnEventData turnData)
    {
      var playerView = _gameView.PlayersView[turnData.PlayerIdx];
      var playerTurn = turnData.PlayerTurn;
      playerView.CardsDeck.Set(playerTurn.PlayDeckCount);
      playerView.SidePile.Set(playerTurn.SidePileCount);
      playerView.WarPile.Set(playerTurn.HiddenCount);
      
      if (playerTurn.RevealedCard.HasValue)
        playerView.RevealedCard.Set(playerTurn.RevealedCard.Value);
    }

    private void Prepare()
    {
      _gameView.InitialDeckView.Set(Enum.GetValues(typeof(CardRank)).Length * Enum.GetValues(typeof(CardSuit)).Length);
      foreach (var playerView in _gameView.PlayersView)
      {
        playerView.CardsDeck.Set(0);
        playerView.SidePile.Set(0);
        playerView.WarPile.Set(0);
        playerView.RevealedCard.Set(null);
        playerView.CardsDeckAnimation.Set(0);
        playerView.CardsDeckAnimationExtra.Set(0);
        playerView.RevealedCardAnimation.Set(null);
      }
    }
  }
}