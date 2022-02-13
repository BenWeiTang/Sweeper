using System.IO;
using UnityEngine;
using Minesweeper.Saving;

namespace Minesweeper.Scene
{
    public class GameInit : MonoBehaviour
    {
        private void OnEnable()
        {
            CheckFileIntegrity();
            LoadScenes();
        }

        private static void LoadScenes()
        {
            var operation = LevelSystem.LoadSceneSingleAsync(SceneIndex.Persistent);
            operation.completed += ((_) => LevelSystem.LoadSceneAdditiveAsync(SceneIndex.StartMenu));
        }

        private static void CheckFileIntegrity()
        {
            if (File.Exists(Application.dataPath + SettingsSerializer.PATH)) return;

            var newMasterSettings = new MasterSettingsData();
            SettingsSerializer.SaveSettings(newMasterSettings);
        }
    }
}