using System;
using System.Collections.Generic;
using Shared.SharedModel.Data;

namespace Client.Controller.TurnsProcess
{
  public class TurnEventsFeed : ITurnEventsFeed
  {
    public event Action OnAdded; 
    
    private readonly Queue<TurnEventData> _queue = new ();
    
    public bool HasEvent =>
      _queue.Count > 0;

    public void Cleanup() =>
      _queue.Clear();

    public void Enqueue(Queue<TurnEventData> data)
    {
      while (data.Count > 0)
        _queue.Enqueue(data.Dequeue());
      OnAdded?.Invoke();
    }
    
    public TurnEventData Dequeue() =>
      _queue.Dequeue();
  }
}