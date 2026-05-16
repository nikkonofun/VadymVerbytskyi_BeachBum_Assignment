using Client.Model.Data;
using Shared.SharedModel.Data;
using Shared.SharedModel.Dto.MakeTurn;

namespace Client.Model
{
  public class AppModel : IAppModel
  {
    private MatchData _match;

    public void InitializeMatch(int[] playerCardsCount)
    {
      _match = new MatchData(playerCardsCount);
    }

    public int PlayerCount => _match.Players.Length;

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