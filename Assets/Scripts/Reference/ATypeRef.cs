using UnityEngine;

namespace Minesweeper.Reference
{
    public abstract class ATypeRef<T> : ScriptableObject
    {
        public T value;
    }
}
