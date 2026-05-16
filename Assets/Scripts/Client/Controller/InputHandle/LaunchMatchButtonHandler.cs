using System;
using Client.Model.GameCommands;
using Shared.SharedModel.Dto.LaunchMatch;

namespace Client.Controller.InputHandle
{
  public class LaunchMatchButtonHandler : ButtonCommandHandlerBase
  {
    protected override IGameCommand GetCommand(Guid processingRequestGuid)
    {
      return new LaunchMatchCommand(new LaunchMatchRequestDto
      {
        RequestId = processingRequestGuid
      }, AppModel)
      .SetOnOk(response =>
      {
        // TODO: handle ok
      })
      .SetOnError(() =>
      {
        // TODO: handle error
      });
    }
  }
}