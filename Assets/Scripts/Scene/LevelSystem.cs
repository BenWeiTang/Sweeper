using UnityEngine;
using UnityEngine.SceneManagement;

namespace Minesweeper.Scene
{
    public static class LevelSystem
    {
        public static AsyncOperation LoadSceneAdditiveAsync(SceneIndex scene)
        {
            return SceneManager.LoadSceneAsync((int)scene, LoadSceneMode.Additive);
        }

        public static AsyncOperation LoadSceneSingleAsync(SceneIndex scene)
        {
            return SceneManager.LoadSceneAsync((int)scene, LoadSceneMode.Single);
        }

        public static bool IsSameScene(SceneIndex index, UnityEngine.SceneManagement.Scene scene)
        {
            return scene == SceneManager.GetSceneByBuildIndex((int)index);
        }

        public static AsyncOperation UnloadSceneAsync(SceneIndex scene)
        {
            return SceneManager.UnloadSceneAsync((int)scene);
        }
    }
}