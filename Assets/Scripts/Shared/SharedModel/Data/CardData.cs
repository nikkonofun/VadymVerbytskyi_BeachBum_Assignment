namespace Shared.SharedModel.Data
{
  public readonly struct CardData
  {
    public readonly CardRank Rank;
    public readonly CardSuit Suit;

    public CardData(CardRank rank, CardSuit suit)
    {
      Rank = rank;
      Suit = suit;
    }
  }
}