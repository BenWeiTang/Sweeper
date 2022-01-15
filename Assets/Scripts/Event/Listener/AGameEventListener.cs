using UnityEngine;
using UnityEngine.Events;

namespace Minesweeper.Event
{
    public abstract class AGameEventListener<T, E, UER> : MonoBehaviour,
    IGameEventListener<T> where E : AGameEvent<T> where UER : UnityEvent<T>
    {
        public E GameEvent { get { return _gameEvent; } set { _gameEvent = value; } }
        [SerializeField] private E _gameEvent;
        [SerializeField] private UER _unityEventResponse;

        public void OnEventRaised(T item)
        {
            if (_unityEventResponse != null)
            {
                _unityEventResponse.Invoke(item);
            }
        }

        private void OnEnable() 
        {
            if (_gameEvent == null) return;
            GameEvent.RegisterListener(this);
        }

        private void OnDisable() 
        {
            if (_gameEvent == null) return;
            GameEvent.DeregisterListener(this);
        }
    }
}
