using System.Collections.Generic;
using System.Linq;
using Client.Model.Data;
using Shared.SharedModel.Data;

namespace Client.Model
{
  public class AppModel : IAppModel
  {
    private MatchData _match;

    public void InitializeMatch(int playersCount)
    {
      _match = new MatchData(playersCount);
    }

    public int PlayerCount => _match.Players.Length;

    public void UpdatePlayers(Queue<TurnEventData> turnEvents)
    {
      for (var i = 0; i < PlayerCount; i++)
      {
        var lastPlayerEvent = turnEvents.LastOrDefault(x => x.PlayerIdx == i);
        if (lastPlayerEvent != null)
          UpdatePlayer(lastPlayerEvent);
      }
    }

    public void UpdatePlayer(TurnEventData turnEventData)
    {
      _match.Players[turnEventData.PlayerIdx].Update(turnEventData.PlayerTurn); 
    }

    public void UpdateMatchResult(MatchResultData matchResultData)
    {
      if (matchResultData == null)
        return;

      _match.MatchResult = matchResultData;
    }

    public PlayerData GetPlayerData(int playerIdx)
    {
      return _match.Players[playerIdx];
    }
  }
}