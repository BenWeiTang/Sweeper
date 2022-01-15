using UnityEngine;

namespace Minesweeper.Scene
{
    public class GameInit : MonoBehaviour
    {
        private void OnEnable() 
        {
            var operation = LevelSystem.LoadSceneSingleAsync(SceneIndex.Persistent);
            operation.completed += ((operation) => LevelSystem.LoadSceneAdditiveAsync(SceneIndex.StartMenu));
        }
    }
}