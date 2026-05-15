using System;
using System.Collections.Generic;
using Shared.SharedModel.Data;

namespace Server.ServerModel.Data
{
  public class CardsDeck
  {
    private const int DefaultCapacity = 26;
    
    private readonly List<CardData> _deck;
    private readonly Random _rand = new();
    
    public int Count => _deck.Count;

    public CardsDeck(int capacity = DefaultCapacity)
    {
      _deck = new List<CardData>(capacity);
    }

    public void Push(CardData card)
    {
      _deck.Add(card);
    }

    public CardData Pop()
    {
      var lastIndex = _deck.Count - 1;
      var card = _deck[lastIndex];
      _deck.RemoveAt(lastIndex);
      return card;
    }
    
    public void Reshuffle()
    {
      for (var i = _deck.Count - 1; i > 0; i--)
      {
        var j = _rand.Next(i + 1);
        (_deck[i], _deck[j]) = (_deck[j], _deck[i]);
      }
    }
  }
}