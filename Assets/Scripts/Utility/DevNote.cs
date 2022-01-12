using UnityEngine;

namespace Minesweeper
{
    public class DevNote : MonoBehaviour
    {
        [TextArea(20, 25)]
        [SerializeField] private string Note; 
    }
}
