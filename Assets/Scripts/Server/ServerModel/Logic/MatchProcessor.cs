using System;
using System.Collections.Generic;
using Server.ServerModel.Data;
using Shared.SharedModel.Data;
using Shared.SharedModel.Dto.MakeTurn;

namespace Server.ServerModel.Logic
{
  public class MatchProcessor
  {
    public MakeTurnResponseDto ProcessTurn(ServerMatchData matchData)
    {
      var response = new MakeTurnResponseDto
      {
        TurnEvents = new Queue<TurnEventData>()
      };

      if (matchData.Result != null)
        return response;

      return ProcessTurnImpl(matchData, response);
    }

    private MakeTurnResponseDto ProcessTurnImpl(ServerMatchData matchData, MakeTurnResponseDto response)
    {
      if (!TryTakeNextCards(matchData, response))
      {
        CheckEndMatch(matchData, response);
        return response;
      }
      
      CompareCards(matchData, out var takerIdx);
      if (takerIdx != null)
      {
        TakeToSidePile(matchData, response, takerIdx.Value);
        CheckEndMatch(matchData, response);
        return response;
      }

      PutRevealedCardToWarPile(matchData, response);
      if (!TryTakeUnrevealedForWar(matchData, response))
      {
        CheckEndMatch(matchData, response);
        return response;
      }
      
      return ProcessTurnImpl(matchData, response);
    }

    private bool TryTakeNextCards(ServerMatchData matchData, MakeTurnResponseDto responseDto)
    {
      for (var i = 0; i < matchData.Players.Length; i++)
      {
        var playerIdx = i;
        var player = matchData.Players[playerIdx];

        // When comes from war
        if (player.RevealedOnTable != null)
        {
          player.HiddenOnTable.Push(player.RevealedOnTable.Value);
          player.RevealedOnTable = null;
        }
        
        if (!TryTakeNextPlayerCard(i, matchData, responseDto, card =>
            {
              player.RevealedOnTable = card;
              responseDto.TurnEvents.Enqueue(TurnEventDataHelper.Create(TurnEventKind.RevealTopFromDeck, playerIdx, matchData));
            }))
          return false;
      }

      return true;
    }

    private void CompareCards(ServerMatchData matchData, out int? takerIdx)
    {
      takerIdx = null;
      CardRank? maxRank = null;
      for (var i = 0; i < matchData.Players.Length; i++)
      {
        var playerRank = matchData.Players[i].RevealedOnTable?.Rank;
        if (maxRank == null || maxRank.Value < playerRank)
        {
          takerIdx = i;
          maxRank = playerRank;
        }
        else if (playerRank == maxRank)
        {
          takerIdx = null;
        }
      }
    }

    private void TakeToSidePile(ServerMatchData matchData, MakeTurnResponseDto responseDto, int takerIdx)
    {
      var takerSidePile = matchData.Players[takerIdx].SidePile;
      foreach (var player in matchData.Players)
      {
        while(player.HiddenOnTable.Count > 0)
          takerSidePile.Push(player.HiddenOnTable.Pop());

        if (player.RevealedOnTable != null)
        {
          takerSidePile.Push(player.RevealedOnTable.Value);
          player.RevealedOnTable = null;
        }
      }
      
      responseDto.TurnEvents.Enqueue(TurnEventDataHelper.Create(TurnEventKind.PutToSidePile, takerIdx, matchData));
    }

    private void PutRevealedCardToWarPile(ServerMatchData matchData, MakeTurnResponseDto responseDto)
    {
      foreach (var player in matchData.Players)
      {
        if (player.RevealedOnTable == null)
          continue;
        
        player.HiddenOnTable.Push(player.RevealedOnTable.Value);
        player.RevealedOnTable = null;
      }
    }

    private bool TryTakeUnrevealedForWar(ServerMatchData matchData, MakeTurnResponseDto responseDto)
    {
      const int unrevealedToTake = 3;

      for (var i = 0; i < matchData.Players.Length; i++)
      {
        var playerIdx = i;
        for (var j = 0; j < unrevealedToTake; j++)
        {
          if (!TryTakeNextPlayerCard(i, matchData, responseDto,
              card =>
              {
                matchData.Players[playerIdx].HiddenOnTable.Push(card);
                responseDto.TurnEvents.Enqueue(TurnEventDataHelper.Create(TurnEventKind.TakeUnrevealedForWar, playerIdx, matchData));
              }))
            return false;
        }
      }

      return true;
    }

    private void UseSidePileAsDeck(int playerIdx, ServerMatchData matchData, MakeTurnResponseDto responseDto)
    {
      var player = matchData.Players[playerIdx];
      
      player.SidePile.Reshuffle();
      
      while (player.SidePile.Count > 0)
        player.PlayDeck.Push(player.SidePile.Pop());

      responseDto.TurnEvents.Enqueue(
        TurnEventDataHelper.Create(TurnEventKind.UseSidePileAsDeck, playerIdx, matchData));
    }

    private bool TryTakeNextPlayerCard(int playerIdx, ServerMatchData matchData, MakeTurnResponseDto responseDto,
      Action<CardData> onPopped)
    {
      var player = matchData.Players[playerIdx];

      if (player.PlayDeck.Count == 0 && player.SidePile.Count == 0)
        return false;

      TryUseSidePileAsDeck();

      var revealedCard = player.PlayDeck.Pop();
      onPopped?.Invoke(revealedCard);

      TryUseSidePileAsDeck();

      return true;

      void TryUseSidePileAsDeck()
      {
        if (player.PlayDeck.Count == 0 && player.SidePile.Count != 0)
          UseSidePileAsDeck(playerIdx, matchData, responseDto);
      }
    }
    
    private void CheckEndMatch(ServerMatchData matchData, MakeTurnResponseDto responseDto)
    {
      int? withCardsIdx = null;
      int? withoutCardsIdx = null;
      for (var i = 0; i < matchData.Players.Length; i++)
      {
        var player = matchData.Players[i];
        if (player.PlayDeck.Count == 0 && player.SidePile.Count == 0)
          withoutCardsIdx = i;
        else
          withCardsIdx = i;
      }

      if (withoutCardsIdx == null)
        return;
      
      matchData.EndMatch(withCardsIdx);
      responseDto.MatchResult = matchData.Result;
    }
  }
}