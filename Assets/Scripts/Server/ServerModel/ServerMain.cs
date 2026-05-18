using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Server.ServerModel.Data;
using Server.ServerModel.Logic;
using Shared.SharedModel.Data;
using Shared.SharedModel.Dto.LaunchMatch;
using Shared.SharedModel.Dto.MakeTurn;

namespace Server.ServerModel
{
  public static class ServerMain
  {
    // Server simplified. No segments, no multiple matches, etc...
    private static ServerMatchData _matchData;
    private static MatchProcessor _matchProcessor;
    private static Guid? _startMatchRequestId;
    private static Dictionary<Guid, MakeTurnResponseDto> _history;

    private static readonly Dictionary<string, Func<string, string>> NameToMethod = new()
    {
      { "LaunchMatch", LaunchMatch },
      { "MakeTurn", MakeTurn },
    };

    private static bool IsInitialized => _matchData != null;

    public static string CallMethod(string methodName, string requestJson)
    {
      if (!NameToMethod.TryGetValue(methodName, out var method))
        return null;  // Simplification. Should return 400 normally
      
      return method(requestJson);
    }
    
    private static string LaunchMatch(string requestJson)
    {
      var request = JsonConvert.DeserializeObject<LaunchMatchRequestDto>(requestJson);
      if (_startMatchRequestId != request.RequestId)
      {
        _startMatchRequestId = request.RequestId;

        _matchData = new MatchInitializer().GenerateMatchData();
        _matchProcessor = new MatchProcessor();

        _history = new Dictionary<Guid, MakeTurnResponseDto>();
      }

      var response = new LaunchMatchResponseDto
      {
        TurnEvents = new Queue<TurnEventData>()
      };

      for (var i = 0; i < _matchData.Players.Length; i++)
      {
        response.TurnEvents.Enqueue(new TurnEventData
        {
          EventKind = TurnEventKind.GetCardsFromInitialDeck,
          PlayerIdx = i,
          PlayerTurn = new PlayerTurnData
          {
            PlayDeckCount = _matchData.Players[i].PlayDeck.Count
          }
        });
      }

      return JsonConvert.SerializeObject(response);
    }

    private static string MakeTurn(string requestJson)
    {
      if (!IsInitialized)
        return null;
      
      var request = JsonConvert.DeserializeObject<MakeTurnRequestDto>(requestJson);
      if (_history.TryGetValue(request.RequestId, out var responseOfId))
        return JsonConvert.SerializeObject(responseOfId);

      var response = _matchProcessor.ProcessTurn(_matchData);
      _history.Add(request.RequestId, response);
      
      return JsonConvert.SerializeObject(response);
    }
  }
}