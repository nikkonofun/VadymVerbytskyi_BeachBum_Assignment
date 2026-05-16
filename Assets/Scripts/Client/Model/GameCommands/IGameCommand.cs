using Shared.SharedModel.Dto;

namespace Client.Model.GameCommands
{
  public interface IGameCommand
  {
    public string MethodName { get; }
    public RequestDtoBase RequestDto { get; }
    public ResponseDtoBase DeserializeResponse(string response);
    public void ProcessResponse(ResponseDtoBase response);
  }
}