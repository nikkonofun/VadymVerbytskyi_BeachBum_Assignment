using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Client.View.Notification
{
  public class NotificationManager : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private RectTransform _trans;
    [SerializeField] private Vector3 _initialPos;
    [SerializeField] private Vector3 _finalPos;
    [SerializeField] private float _transparencyChangeSec;
    [SerializeField] private float _transitionSec;
    [SerializeField] private Color _notificationColor;

    private CancellationTokenSource _cancellation;
    
    public void Show(string text)
    {
      if(_cancellation != null)
        Cancel();
      
      _text.text = text;
      _text.color = Color.clear;
      _trans.anchoredPosition = _initialPos;
      _trans.gameObject.SetActive(true);
      
      _cancellation = new CancellationTokenSource();

      Animate().Forget();
    }

    private async UniTask Animate()
    {
      var tasks = new List<UniTask>
      {
        _trans.DOAnchorPos(_finalPos, _transitionSec).ToUniTask(cancellationToken: _cancellation.Token)
          .ContinueWith(() => _text.DOColor(Color.clear, _transparencyChangeSec)),
        _text.DOColor(_notificationColor, _transparencyChangeSec).ToUniTask(cancellationToken: _cancellation.Token)
      };

      await UniTask.WhenAll(tasks);
    }

    private void Cancel()
    {
      _trans.gameObject.SetActive(false);
      
      _cancellation?.Cancel();
      _cancellation?.Dispose();
      _cancellation = null;
    }
    
    private void Awake()
    {
      Cancel();
      DontDestroyOnLoad(gameObject);
    }
  }
}