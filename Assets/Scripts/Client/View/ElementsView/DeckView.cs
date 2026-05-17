using TMPro;
using UnityEngine;

namespace Client.View.ElementsView
{
  public class DeckView : ElementViewBase
  {
    [SerializeField] private GameObject _obj;
    [SerializeField] private TextMeshProUGUI _cardsCount;

    public void Set(int cardsCount)
    {
      _obj.SetActive(cardsCount > 0);
      _cardsCount.text = cardsCount.ToString();
    }

    public int GetCardsCount()
    {
      if (!int.TryParse(_cardsCount.text, out var val))
        return 0;

      return val;
    } 
  }
}