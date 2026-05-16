using Shared.SharedModel.Data;

namespace Client.Model.Data
{
  public class MatchData
  {
    public PlayerData[] Players { get; }
    public MatchResultData MatchResult { get; set; }

    public MatchData(int[] playerCardsCount)
    {
      Players = new PlayerData[playerCardsCount.Length];
      for (var i = 0; i < playerCardsCount.Length; i++)
        Players[i] = new PlayerData(playerCardsCount[i]);
    }
  }
}