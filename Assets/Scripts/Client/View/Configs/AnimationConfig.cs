using UnityEngine;

[CreateAssetMenu(fileName = "AnimationConfig", menuName = "Configs/AnimationConfig", order = 0)]
public class AnimationConfig : ScriptableObject
{
  [SerializeField] private float _animationSpeed = 1.0f;
  [SerializeField] private float _cardTransitionSec = 0.65f;
  [SerializeField] private float _cardRotationSec = 0.4f;
  [SerializeField] private float _deckShuffleSec = 0.75f;
  [SerializeField] private float _cardFlipSec = 0.25f;

  private void Awake()
  {
    if (_animationSpeed <= 0.0f)
      _animationSpeed = 0.001f;
  }

  public float CardTransitionSec => _cardTransitionSec / _animationSpeed;
  public float CardRotationSec => _cardRotationSec / _animationSpeed;
  public float DeckShuffleSec => _deckShuffleSec / _animationSpeed;
  public float CardFlipSec => _cardFlipSec / _animationSpeed;
}
