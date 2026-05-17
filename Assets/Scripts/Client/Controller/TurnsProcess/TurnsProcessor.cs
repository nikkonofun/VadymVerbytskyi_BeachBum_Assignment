using System;
using Client.View.TurnsAnimation;
using Zenject;

namespace Client.Controller.TurnsProcess
{
  public class TurnsProcessor : IInitializable, IDisposable
  {
    private readonly ITurnsAnimator _animator;
    private readonly TurnEventsFeed _turnEventsFeed;
    
    private bool _isProcessing;

    [Inject]
    public TurnsProcessor(ITurnsAnimator turnsAnimator, TurnEventsFeed turnEventsFeed)
    {
      _animator = turnsAnimator;
      _turnEventsFeed = turnEventsFeed;
    }
    
    public void Initialize()
    {
      _turnEventsFeed.OnAdded += TryRunNext;
      TryRunNext();
    }
    
    public void Dispose()
    {
      _turnEventsFeed.OnAdded -= TryRunNext;
    }
    
    private void TryRunNext()
    {
      if (!_turnEventsFeed.HasEvent)
      {
        _isProcessing = false;
        return;
      }

      var next = _turnEventsFeed.Dequeue();
      _animator.Animate(next, OnAnimationFinished);
      _isProcessing = true;
    }
    
    private void OnAnimationFinished()
    {
      TryRunNext();
    }
  }
}