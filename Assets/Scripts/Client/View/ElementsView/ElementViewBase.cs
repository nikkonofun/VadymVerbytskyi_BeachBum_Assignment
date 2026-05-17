using UnityEngine;

namespace Client.View.ElementsView
{
  public abstract class ElementViewBase : MonoBehaviour
  {
    [SerializeField] private RectTransform _trans;
    
    public RectTransform GetTransform()
    {
      return _trans;
    }

    public Vector2 GetPosition()
    {
      return _trans.anchoredPosition;
    }

    public Vector3 GetRotation()
    {
      return _trans.localEulerAngles;
    }

    public Vector3 GetScale()
    {
      return _trans.localScale;
    }
  }
}