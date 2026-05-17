using System;
using Shared.SharedModel.Data;

namespace Client.View.TurnsAnimation
{
  public interface ITurnsAnimator
  {
    public void Animate(TurnEventData turnData, Action onAnimationFinished);
  }
}