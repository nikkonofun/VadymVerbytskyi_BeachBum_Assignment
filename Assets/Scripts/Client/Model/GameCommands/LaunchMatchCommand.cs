using Shared.SharedModel.Dto.LaunchMatch;

namespace Client.Model.GameCommands
{
  public class LaunchMatchCommand : GameCommandBase<LaunchMatchRequestDto, LaunchMatchResponseDto>
  {
    public LaunchMatchCommand(LaunchMatchRequestDto request, IAppModel appModel) : base(request,  appModel) { }

    public override string MethodName => "LaunchMatch";
    
    protected override void ProcessResponseImpl(LaunchMatchResponseDto response)
    {
      AppModel.InitializeMatch(response.PlayerCardsCount);
    }
  }
}