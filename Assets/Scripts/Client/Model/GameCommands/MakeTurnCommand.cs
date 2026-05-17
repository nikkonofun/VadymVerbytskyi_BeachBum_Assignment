using Client.Controller.TurnsProcess;
using Shared.SharedModel.Dto.MakeTurn;

namespace Client.Model.GameCommands
{
  public class MakeTurnCommand : GameCommandBase<MakeTurnRequestDto, MakeTurnResponseDto>
  {
    private readonly ITurnEventsFeed _turnEventsFeed;
    
    public MakeTurnCommand(MakeTurnRequestDto request, IAppModel appModel, ITurnEventsFeed turnEventsFeed)
      : base(request, appModel)
    {
      _turnEventsFeed = turnEventsFeed;
    }

    public override string MethodName => "MakeTurn";
    
    protected override void ProcessResponseImpl(MakeTurnResponseDto response)
    {
      AppModel.UpdatePlayers(response.TurnEvents);
      AppModel.UpdateMatchResult(response.MatchResult);
      _turnEventsFeed.Enqueue(response.TurnEvents);
    }
  }
}