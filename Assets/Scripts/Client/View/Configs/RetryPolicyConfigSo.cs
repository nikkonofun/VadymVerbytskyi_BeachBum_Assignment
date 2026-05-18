using UnityEngine;

namespace Client.View.Configs
{
  [CreateAssetMenu(fileName = "RetryPolicyConfig", menuName = "Configs/RetryPolicyConfig", order = 0)]
  public class RetryPolicyConfigSo : ScriptableObject
  {
    public float[] AttemptDelays = { 1.0f, 2.0f, 4.0f };
    public float TimeoutSec = 30;
  }
}