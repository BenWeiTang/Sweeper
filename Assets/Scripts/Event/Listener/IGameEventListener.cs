namespace Minesweeper.Event
{
    public interface IGameEventListener<T>
    {
        void OnEventRaised(T item);
    }
}
