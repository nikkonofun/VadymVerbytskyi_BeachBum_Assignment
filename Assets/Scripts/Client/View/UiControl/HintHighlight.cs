using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Client.View.UiControl
{
  public class HintHighlight : MonoBehaviour
  {
    [SerializeField] private Image _highlightImg;
    [SerializeField] private int _highlightAfterMs = 3000;
    [SerializeField] private float _scaleFrom = 1.0f;
    [SerializeField] private float _scaleTo = 1.15f;
    [SerializeField] private float _scaleDurationSec = 0.65f;
      
    private CancellationTokenSource _cancellationToken;
    private Tween _tweenHighlight;
    
    // TODO: connect with controller
    public void ShowHintDelayed()
    {
      if (_cancellationToken != null)
        return;
      
      _cancellationToken = new CancellationTokenSource();
      ShowHintDelayedAsync().Forget();
    }

    public void HideHint()
    {
      _tweenHighlight?.Kill();
      _highlightImg.enabled = false;
      
      _cancellationToken?.Cancel();
      _cancellationToken?.Dispose();
      _cancellationToken = null;
    }

    private void Awake()
    {
      HideHint();
      ShowHintDelayed();
    }

    private async UniTaskVoid ShowHintDelayedAsync()
    {
      await UniTask.Delay(_highlightAfterMs, cancellationToken: _cancellationToken.Token);

      _highlightImg.enabled = true;

      var currentScale = transform.localScale;
      transform.localScale = new Vector3(_scaleFrom, _scaleFrom, currentScale.z);
      _tweenHighlight = transform
        .DOScale(new Vector3(_scaleTo, _scaleTo, currentScale.z), _scaleDurationSec)
        .SetEase(Ease.Linear)
        .SetLoops(-1, LoopType.Yoyo);
    }
  }
}