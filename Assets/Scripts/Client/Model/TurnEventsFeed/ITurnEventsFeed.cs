using System;
using System.Collections.Generic;
using Shared.SharedModel.Data;

namespace Client.Controller.TurnsProcess
{
  public interface  ITurnEventsFeed
  {
    event Action OnAdded; 
    bool HasEvent { get; }
    void Cleanup();
    void Enqueue(Queue<TurnEventData> data);
    TurnEventData Dequeue();
  }
}