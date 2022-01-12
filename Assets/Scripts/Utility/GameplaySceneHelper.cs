using UnityEngine;
using Minesweeper.Core;
using Minesweeper.Event;

namespace Minesweeper
{
#if UNITY_EDITOR
    public class GameplaySceneHelper : MonoBehaviour
    {
        // This is only useful when we play the game straight from the Gameplay scene
        public void InitFromGameplayScene()
        {
            UIManager.Instance.SudoTurnOffStartPanel();
        }
    }
#endif
}
