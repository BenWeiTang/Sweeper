namespace Minesweeper.Core
{
    public interface ISpot
    {
        void Dig();
        void Mark();
        void ClearNear();
    }
}