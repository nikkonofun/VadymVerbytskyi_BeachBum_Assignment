using System.Collections.Generic;
using Shared.SharedModel.Data;

namespace Client.Model.Data
{
  public class MatchData
  {
    public PlayerData[] Players { get; }
    public MatchResultData MatchResult { get; set; }
    public List<TurnEventData> TurnsHistory { get; }

    public MatchData(int playersCount)
    {
      Players = new PlayerData[playersCount];
      for (var i = 0; i < playersCount; i++)
        Players[i] = new PlayerData();
    }
  }
}