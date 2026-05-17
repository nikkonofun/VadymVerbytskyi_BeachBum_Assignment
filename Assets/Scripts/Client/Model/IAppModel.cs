using System.Collections.Generic;
using Client.Model.Data;
using Shared.SharedModel.Data;
using Shared.SharedModel.Dto.MakeTurn;

namespace Client.Model
{
  public interface IAppModel
  {
    void InitializeMatch(int playersCount);
    int PlayerCount { get; }
    void UpdatePlayers(Queue<TurnEventData> turnEvents);
    void UpdatePlayer(TurnEventData turnEventData);
    void UpdateMatchResult(MatchResultData matchResultData);
    PlayerData GetPlayerData(int playerIdx);
  }
}