using System;
using Client.View.TurnsAnimation;
using Zenject;

namespace Client.Controller.TurnsProcess
{
  public class TurnsProcessor : IInitializable, IDisposable
  {
    private readonly ITurnsAnimator _animator;
    private readonly ITurnEventsFeed _turnEventsFeed;
    
    private bool _isProcessing;

    [Inject]
    public TurnsProcessor(ITurnsAnimator turnsAnimator, ITurnEventsFeed turnEventsFeed)
    {
      _animator = turnsAnimator;
      _turnEventsFeed = turnEventsFeed;
    }
    
    public void Initialize()
    {
      TryRunNext();
      _turnEventsFeed.OnAdded += TryRunNext;
    }
    
    public void Dispose()
    {
      _turnEventsFeed.OnAdded -= TryRunNext;
    }
    
    private void TryRunNext()
    {
      if (_isProcessing)
        return;
      
      if (!_turnEventsFeed.HasEvent)
      {
        _isProcessing = false;
        return;
      }
      
      _isProcessing = true;

      var next = _turnEventsFeed.Dequeue();
      _animator.Animate(next, OnAnimationFinished);
    }
    
    private void OnAnimationFinished()
    {
      _isProcessing = false;
      TryRunNext();
    }
  }
}