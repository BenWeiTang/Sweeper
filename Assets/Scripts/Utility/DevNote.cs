using UnityEngine;

namespace Minesweeper.Utility
{
    public class DevNote : MonoBehaviour
    {
        [TextArea(20, 25)]
        [SerializeField] private string Note; 
    }
}
