using System;
using Server.ServerModel.Data;
using Shared.SharedModel.Data;

namespace Server.ServerModel.Logic
{
  public class MatchInitializer
  {
    public ServerMatchData GenerateMatchData()
    {
      var matchData = new ServerMatchData();
      
      var ranks = (CardRank[])Enum.GetValues(typeof(CardRank));
      var suits = (CardSuit[])Enum.GetValues(typeof(CardSuit));
      
      var allCards = new CardsDeck(ranks.Length * suits.Length);
      foreach (var suit in suits)
        foreach (var rank in ranks)
          allCards.Push(new CardData(rank, suit));
      
      allCards.Reshuffle();

      var initialCount = allCards.Count;
      for (var i = 0; i < initialCount; i++)
        matchData.Players[i % matchData.Players.Length].PlayDeck.Push(allCards.Pop());

      return matchData;
    }
  }
}