using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Minesweeper.Scene;
using Minesweeper.Event;

namespace Minesweeper.Core
{
    public class GameManager : AManager<GameManager>
    {
        public GameState CurrentState { get; set; } = GameState.None;


        [Header("Setting")]
        [SerializeField] private Layout _defaultLayout;

        [Header("Event")]
        [SerializeField] private VoidEvent GameplaySceneLoaded;
        [SerializeField] private VoidEvent GamePause;
        [SerializeField] private VoidEvent GameResume;
        [SerializeField] private BoolEvent PostGameEnter;

        private Layout _layout;
        public Layout CurrentLayout
        {
            get
            {
                if (_layout == null)
                    _layout = _defaultLayout;
                return _layout;
            }
            private set { }
        }


        protected override void Awake()
        {
            base.Awake();
            CurrentState = GameState.None;

            SceneManager.sceneLoaded += (scene, mode) =>
            {
                // Whenever entering Gameplay, 
                if (LevelSystem.IsSameScene(SceneIndex.Gameplay, scene))
                {
                    CurrentState = GameState.PreGame;
                    GameplaySceneLoaded.Raise();
                }
            };
        }

        // Should only be called by the UI Manager
        public async Task StartNewGame() => await UnloadThenLoadScene(SceneIndex.StartMenu, SceneIndex.Gameplay);

        // Should only be called by the UI Manager
        public async Task RestartGame() => await UnloadThenLoadScene(SceneIndex.Gameplay, SceneIndex.Gameplay);

        // Should only be called by the UI Manager
        public async Task BackToMenu()
        {
            CurrentState = GameState.None;
            await UnloadThenLoadScene(SceneIndex.Gameplay, SceneIndex.StartMenu);
        }

        public void PauseGame()
        {
            GamePause.Raise();
            CurrentState = GameState.Pause;
        }

        public void ResumeGame()
        {
            GameResume.Raise();
            CurrentState = GameState.InGame;
        }

        public void OnGameReady()
        {
            CurrentState = GameState.InGame;
        }

        public void OnGameFinished(bool won)
        {
            CurrentState = GameState.PostGame;
            PostGameEnter.Raise(won);
        }

        private async Task UnloadThenLoadScene(SceneIndex toUnload, SceneIndex toLoad)
        {
            var unloadOperation = LevelSystem.UnloadSceneAsync(toUnload);
            while (!unloadOperation.isDone)
            {
                await Task.Yield();
            }

            var loadOperation = LevelSystem.LoadSceneAdditiveAsync(toLoad);
            while (!loadOperation.isDone)
            {
                await Task.Yield();
            }
        }
    }

    public enum GameState
    {

        // When we are not in the Gameplay scene, e.g., in StartMenu or Credit. This is the default state
        None,

        // When in the Gameplay scene but before the start of game, useful for animations to play before game
        PreGame,

        // When in the Gameplay scene and when on pause
        Pause,

        // When in the Gameplay scene and game is playing
        InGame,

        // When in the Gameplay scene but after the end of game (win or lose), useful for animations to play after game
        PostGame
    }
}