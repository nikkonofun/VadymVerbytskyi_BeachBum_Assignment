using Client.Model.Data;
using Shared.SharedModel.Data;
using Shared.SharedModel.Dto.MakeTurn;

namespace Client.Model
{
  public interface IAppModel
  {
    void InitializeMatch(int[] playerCardsCount);
    int PlayerCount { get; }
    void UpdatePlayer(TurnEventData turnEventData);
    void UpdateMatchResult(MatchResultData matchResultData);
    PlayerData GetPlayerData(int playerIdx);
  }
}