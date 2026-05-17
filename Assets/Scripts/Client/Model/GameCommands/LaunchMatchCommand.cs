using System.Linq;
using Client.Controller.TurnsProcess;
using Shared.SharedModel.Dto.LaunchMatch;

namespace Client.Model.GameCommands
{
  public class LaunchMatchCommand : GameCommandBase<LaunchMatchRequestDto, LaunchMatchResponseDto>
  {
    private readonly ITurnEventsFeed _turnEventsFeed;

    public LaunchMatchCommand(LaunchMatchRequestDto request, IAppModel appModel, ITurnEventsFeed turnEventsFeed)
      : base(request, appModel)
    {
      _turnEventsFeed = turnEventsFeed;
    }

    public override string MethodName => "LaunchMatch";
    
    protected override void ProcessResponseImpl(LaunchMatchResponseDto response)
    {
      _turnEventsFeed.Cleanup();
      _turnEventsFeed.Enqueue(response.TurnEvents);
      AppModel.InitializeMatch(response.TurnEvents.Select(x => x.PlayerIdx).Distinct().Count());
      AppModel.UpdatePlayers(response.TurnEvents);
    }
  }
}