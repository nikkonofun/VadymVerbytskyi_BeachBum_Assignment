namespace Client.Model.RequestResponseManagement
{
  public class RetryPolicyConfig
  {
    public float[] AttemptDelays { get; private set; }
    public float TimeoutSec { get; private set; }

    public RetryPolicyConfig(float[] attemptDelays, float timeoutSec)
    {
      AttemptDelays = attemptDelays;
      TimeoutSec = timeoutSec;
    }
  }
}