using UnityEngine;

namespace Minesweeper.Event
{
    [CreateAssetMenu(fileName = "New Void Event", menuName = "3D Minesweeper/Game Event/Void Event")]
    public class VoidEvent : AGameEvent<Void>
    {
        public void Raise() => Raise(new Void());
    }
}
