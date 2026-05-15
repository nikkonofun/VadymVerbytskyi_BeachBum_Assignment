using System.Collections.Generic;
using Server.ServerModel.Data;
using Server.ServerModel.Logic;
using Shared.SharedModel.Data;
using Shared.SharedModel.Dto;
using Shared.SharedModel.Dto.LaunchMatch;
using Shared.SharedModel.Dto.MakeTurn;

namespace Server.ServerModel
{
  // TODO: make asynchronous
  public static class ServerMain
  {
    private static ServerMatchData _matchData;
    private static MatchProcessor _matchProcessor;
    
    public static LaunchMatchResponseDto LaunchMatch(LaunchMatchRequestDto request)
    {
      _matchData = new MatchInitializer().GenerateMatchData();
      _matchProcessor = new MatchProcessor();

      var response = new LaunchMatchResponseDto
      {
        TurnEvents = new Queue<TurnEventData>()
      };

      for (var i = 0; i < _matchData.Players.Length; i++)
        response.TurnEvents.Enqueue(TurnEventDataHelper.Create(TurnEventKind.GetCardsFromInitialDeck, i, _matchData));
      
      return response;
    }

    public static MakeTurnResponseDto MakeTurn(MakeTurnRequestDto request)
    {
      return _matchProcessor.ProcessTurn(_matchData);
    }
  }
}