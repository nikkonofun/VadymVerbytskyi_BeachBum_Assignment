using UnityEngine;

namespace Client.View.Configs
{
  [CreateAssetMenu(fileName = "FakeNetworkConfig", menuName = "Configs/FakeNetworkConfig", order = 0)]
  public class FakeNetworkConfigSo : ScriptableObject
  {
    public int MinDelayMs = 0;
    public int MaxDelayMs = 0;
    public float NetworkFailureChance = 0.5f; // 0..1 -> 0%..100%
    public float RequestLossChance = 0.1f; // 0..1 -> 0%..100%
    public int TimeoutMs = 5000;
  }
}