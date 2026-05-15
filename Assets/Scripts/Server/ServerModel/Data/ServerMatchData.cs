using Shared.SharedModel.Data;

namespace Server.ServerModel.Data
{
  public class ServerMatchData
  {
    public const int MaxPlayers = 2;
    
    public PlayerData[] Players { get; }
    public MatchResultData Result { get; private set; }
    
    public ServerMatchData()
    {
      Players = new PlayerData[MaxPlayers];
      for (var i = 0; i < MaxPlayers; i++)
        Players[i] = new PlayerData();
    }

    public void EndMatch(int? winnerPlayerIdx)
    {
      Result = new MatchResultData(winnerPlayerIdx);
    }
  }
}