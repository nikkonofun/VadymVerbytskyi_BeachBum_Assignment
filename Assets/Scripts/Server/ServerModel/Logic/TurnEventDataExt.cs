using Server.ServerModel.Data;
using Shared.SharedModel.Data;
using Shared.SharedModel.Dto;

namespace Server.ServerModel.Logic
{
  public static class TurnEventDataHelper
  {
    public static TurnEventData Create(TurnEventKind kind, int playerIdx, ServerMatchData matchData)
    {
      var player = matchData.Players[playerIdx];
      return new TurnEventData
      {
        EventKind = kind,
        PlayerIdx = playerIdx,
        PlayerTurn = new PlayerTurnData
        {
          HiddenCount = player.HiddenOnTable.Count,
          RevealedCard = player.RevealedOnTable,
          PlayDeckCount = player.PlayDeck.Count,
          SidePileCount = player.SidePile.Count
        }
      };
    }
  }
}