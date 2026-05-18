using Shared.SharedModel.Data;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace Client.View.ElementsView
{
  public class RevealedCardView : ElementViewBase
  {
    [SerializeField] private GameObject _obj;
    [SerializeField] private Image _cardImage;
    [SerializeField] private SpriteAtlas _cardsAtlas;

    public void Set(CardData? cardData)
    {
      _obj.SetActive(cardData != null);
      
      if (cardData == null)
        return;
        
      var spriteName = $"{cardData.Value.Suit.ToString()}{cardData.Value.Rank.ToString()}";
      _cardImage.sprite = _cardsAtlas.GetSprite(spriteName);
    }

    public void SetSprite(Sprite sprite)
    {
      _obj.SetActive(sprite != null);
      _cardImage.sprite = sprite; 
    }

    public Sprite GetSprite() =>
      _cardImage.sprite;
  }
}