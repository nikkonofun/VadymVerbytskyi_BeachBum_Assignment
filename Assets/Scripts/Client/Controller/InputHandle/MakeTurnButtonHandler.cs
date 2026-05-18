using System;
using Client.Model.GameCommands;
using Shared.SharedModel.Dto.MakeTurn;

namespace Client.Controller.InputHandle
{
  public class MakeTurnButtonHandler : ButtonCommandHandlerBase
  {
    protected override IGameCommand GetCommand(Guid processingRequestGuid)
    {
      return new MakeTurnCommand(new MakeTurnRequestDto
        {
          RequestId = processingRequestGuid
        }, AppModel, TurnEventsFeed)
        .SetOnError(ShowNotificationError);
    }
  }
}