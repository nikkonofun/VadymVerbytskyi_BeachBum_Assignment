namespace Shared.ClientServerFakeLayer
{
  public class FakeNetworkConfig
  {
    public int MinDelayMs { get; private set; }
    public int MaxDelayMs { get; private set; }
    public float NetworkFailureChance { get; private set; }
    public float RequestLossChance { get; private set; }
    public int TimeoutMs { get; private set; }

    public FakeNetworkConfig(int minDelayMs, int maxDelayMs, float networkFailureChance, float requestLossChance,
      int timeoutMs)
    {
      MinDelayMs = minDelayMs;
      MaxDelayMs = maxDelayMs;
      NetworkFailureChance = networkFailureChance;
      RequestLossChance = requestLossChance;
      TimeoutMs = timeoutMs;
    }
  }
}