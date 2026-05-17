using Shared.SharedModel.Data;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace Client.View.ElementsView
{
  public class RevealedCardView : MonoBehaviour
  {
    [SerializeField] private Image _cardImage;
    [SerializeField] private SpriteAtlas _cardsAtlas;

    public void Set(CardData cardData)
    {
      var spriteName = $"{cardData.Suit.ToString()}{cardData.Rank.ToString()}";
      _cardImage.sprite = _cardsAtlas.GetSprite(spriteName);
    }
  }
}