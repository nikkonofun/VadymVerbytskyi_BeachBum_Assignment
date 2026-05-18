using System;
using Client.Model;
using Client.View.MatchEndAnimation;
using Client.View.TurnsAnimation;
using Zenject;

namespace Client.Controller.TurnsProcess
{
  public class TurnsProcessor : IInitializable, IDisposable
  {
    private readonly ITurnsAnimator _turnsAnimator;
    private readonly IMatchEndAnimator _matchEndAnimator;
    private readonly ITurnEventsFeed _turnEventsFeed;
    private readonly IAppModel _appModel;
    
    private bool _isProcessing;

    [Inject]
    public TurnsProcessor(ITurnsAnimator turnsAnimator, ITurnEventsFeed turnEventsFeed, 
      IMatchEndAnimator matchEndAnimator, IAppModel appModel)
    {
      _turnsAnimator = turnsAnimator;
      _matchEndAnimator = matchEndAnimator;
      _turnEventsFeed = turnEventsFeed;
      _appModel = appModel;
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
        CheckMatchEnd();
        return;
      }
      
      _isProcessing = true;

      var next = _turnEventsFeed.Dequeue();
      _turnsAnimator.Animate(next, OnAnimationFinished);
    }
    
    private void OnAnimationFinished()
    {
      _isProcessing = false;
      TryRunNext();
    }

    private void CheckMatchEnd()
    {
      if (!_appModel.IsMatchFinished(out var playerWinnerIdx))
        return;

      MatchEndKind kind;
      if (playerWinnerIdx == 0)
        kind = MatchEndKind.Win;
      else if(playerWinnerIdx == null)
        kind = MatchEndKind.Draw;
      else
        kind = MatchEndKind.Lose;
      
      _matchEndAnimator.Animate(kind);
    }
  }
}