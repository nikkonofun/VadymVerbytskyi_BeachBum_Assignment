using Client.View.UiControl;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Client.View.MatchEndAnimation
{
  public class MatchEndAnimator : MonoBehaviour, IMatchEndAnimator
  {
    [SerializeField] private GameObject _root;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private AnimationConfig _animationConfig;
    [SerializeField] private HintHighlight _hintHighlight;
    
    public void Animate(MatchEndKind kind)
    {
      _hintHighlight.gameObject.SetActive(false);
      
      _canvasGroup.alpha = 0.0f;
      _text.text = kind.ToString().ToUpper();
        
      _root.SetActive(true);
      
      _canvasGroup.DOFade(1.0f, _animationConfig.MatchEndRevealSec);
    }
  }
}