using Client.View.Misc;
using UnityEngine;
using UnityEngine.UI;

namespace Client.View
{
  [ExecuteAlways]
  public class BackgroundAspectConfigurator : MonoBehaviour
  {
    [SerializeField] private RectTransform _trans;
    [SerializeField] private float _landscapeRotation;
    [SerializeField] private float _portraitRotation;
  
    private int _lastWidth;
    private int _lastHeight;

    private void Start() =>
      SaveDimensions();
    
    private void Update()
    {
      if (Screen.width == _lastWidth && Screen.height == _lastHeight)
        return;
    
      SaveDimensions();
    
      var newZ = _lastWidth < _lastHeight 
        ? _portraitRotation 
        : _landscapeRotation;
      
      _trans.localEulerAngles = _trans.localEulerAngles.ChangeZ(newZ);
    }
  
    private void SaveDimensions()
    {
      _lastWidth = Screen.width;
      _lastHeight = Screen.height;
    }
  }
}
