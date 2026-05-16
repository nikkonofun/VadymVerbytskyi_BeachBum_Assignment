using System.Linq;
using Shared.SharedModel.Dto.MakeTurn;

namespace Client.Model.GameCommands
{
  public class MakeTurnCommand : GameCommandBase<MakeTurnRequestDto, MakeTurnResponseDto>
  {
    public MakeTurnCommand(MakeTurnRequestDto request, IAppModel appModel) : base(request, appModel) { }

    public override string MethodName => "MakeTurn";
    
    protected override void ProcessResponseImpl(MakeTurnResponseDto response)
    {
      for (var i = 0; i < AppModel.PlayerCount; i++)
      {
        var lastPlayerEvent = response.TurnEvents.LastOrDefault(x => x.PlayerIdx == i);
        if (lastPlayerEvent != null)
          AppModel.UpdatePlayer(lastPlayerEvent);
      }

      AppModel.UpdateMatchResult(response.MatchResult);
    }
  }
}