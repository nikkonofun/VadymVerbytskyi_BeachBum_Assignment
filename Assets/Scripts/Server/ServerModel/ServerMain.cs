using Server.ServerModel.Data;
using Server.ServerModel.Logic;
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
    }

    public static MakeTurnResponseDto MakeTurn(MakeTurnRequestDto request)
    {
      _matchProcessor.ProcessTurn(_matchData);
    }
  }
}