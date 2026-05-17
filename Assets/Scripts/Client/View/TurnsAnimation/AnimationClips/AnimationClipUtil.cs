using Client.View.ElementsView;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Client.View.TurnsAnimation.AnimationClips
{
  public static class AnimationClipUtil
  {
    public static void SetTransformation(ElementViewBase receiver, ElementViewBase origin, 
      float? customRotationZ = null, float? customScaleX = null)
    {
      var trans = receiver.GetTransform();
      trans.anchoredPosition = origin.GetPosition();

      var originRotation = origin.GetRotation();
      trans.localEulerAngles = new Vector3(originRotation.x, originRotation.y, customRotationZ ?? originRotation.z);
      
      var originScale = origin.GetScale();
      trans.localScale = new Vector3(customScaleX ?? originScale.x, originScale.y, originScale.z);
    }

    public static UniTask MoveToAsync(ElementViewBase current, ElementViewBase target, float transitionSec)
    {
      return current.GetTransform()
        .DOAnchorPos(target.GetPosition(), transitionSec)
        .ToUniTask();
    }
    
    public static UniTask RotateToAsync(ElementViewBase current, ElementViewBase target, float transitionSec)
    {
      return current.GetTransform()
        .DORotate(target.GetRotation(), transitionSec)
        .ToUniTask();
    }
    
    public static UniTask RotateToAsync(ElementViewBase current, float targetZ, float transitionSec)
    {
      var rotation = current.GetRotation();
      return current.GetTransform()
        .DORotate(new Vector3(rotation.x, rotation.y, targetZ), transitionSec)
        .ToUniTask();
    }

    public static UniTask FlipHideAsync(ElementViewBase current, float transitionSec) =>
      FlipAsync(current, transitionSec, 0.0f);
    
    public static UniTask FlipShowAsync(ElementViewBase current, float transitionSec) =>
      FlipAsync(current, transitionSec, 1.0f);

    public static void ResetAnimationElements(GameView _gameView)
    {
      foreach (var playerView in _gameView.PlayersView)
      {
        playerView.CardsDeckAnimation.Set(0);
        playerView.CardsDeckAnimationExtra.Set(0);
        playerView.RevealedCardAnimation.Set(null);
      }
    }

    private static UniTask FlipAsync(ElementViewBase current, float transitionSec, float scaleX)
    {
      return current.GetTransform()
        .DOScaleX(scaleX, transitionSec)
        .ToUniTask();
    }
  }
}