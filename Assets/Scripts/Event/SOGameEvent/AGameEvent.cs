using System.Collections.Generic;
using UnityEngine;

namespace Minesweeper.Event
{
    public abstract class AGameEvent<T> : ScriptableObject
    {
#if UNITY_EDITOR
        [TextArea(5, 10), SerializeField] private string DevNote;
#endif

        private readonly List<IGameEventListener<T>> _eventListeners  = new List<IGameEventListener<T>>();

        public void Raise(T item)
        {
            for (int i = _eventListeners.Count - 1; i >= 0 ; i--)
            {
                _eventListeners[i].OnEventRaised(item);
            }
        }

        public void RegisterListener(IGameEventListener<T> listener)
        {
            if (!_eventListeners.Contains(listener))
            {
                _eventListeners.Add(listener);
            }
        }

        public void DeregisterListener(IGameEventListener<T> listener)
        {
            if (_eventListeners.Contains(listener))
            {
                _eventListeners.Remove(listener);
            }
        }
    }
}
