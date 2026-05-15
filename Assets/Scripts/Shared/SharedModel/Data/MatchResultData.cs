namespace Shared.SharedModel.Data
{
  public class MatchResultData
  {
    public int? WinnerPlayerIdx { get; }

    public MatchResultData(int? winnerPlayerIdx)
    {
      WinnerPlayerIdx = winnerPlayerIdx;
    }
  }
}