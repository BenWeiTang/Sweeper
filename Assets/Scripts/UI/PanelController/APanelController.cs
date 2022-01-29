using UnityEngine;

namespace Minesweeper.UI.PanelController
{
    public abstract class APanelController : MonoBehaviour
    {
        public abstract PanelType Type {get;}
    }

    public enum PanelType
    {
        None,
        Welcome,
        StartMenu,
        Settings,
        Pause,
        Win,
        Lose,
        Loading,
    }
}
