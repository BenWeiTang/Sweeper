using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Minesweeper.Event;
using Minesweeper.Animation;

namespace Minesweeper.Core
{
    public class UIManager : AManager<UIManager>
    {
        [Header("Event")]
        [SerializeField] private VoidEvent FadeBlindOutComplete;

        [Header("Fade Blind")]
        [SerializeField] private CanvasGroup _blind;
        [SerializeField] private CanvasGroupFade _blindFadeInAnim;
        [SerializeField] private CanvasGroupFade _blindFadeOutAnim;

        [Header("Panels")]
        [SerializeField] private CanvasGroupFade _panelFadeInAnim;
        [SerializeField] private CanvasGroupFade _panelFadeOutAnim;
        [SerializeField] private GameObject _startMenuPanel;
        [SerializeField] private GameObject _settingsPanel;
        [SerializeField] private GameObject _pausePanel;
        [SerializeField] private GameObject _winPanel;
        [SerializeField] private GameObject _losePanel;
        [SerializeField] private GameObject _loadingPanel;

        private GameObject _currentActivePanel;
        private bool _isFirstTime = true;

        protected override void Awake()
        {
            base.Awake();

            SceneManager.sceneLoaded += (scene, mode) =>
            {
                _blind.alpha = 1f;
                _blind.blocksRaycasts = true;

                if (_isFirstTime)
                {
                    _isFirstTime = false;
                    StartCoroutine(OnFirstTimeLoaded());
                }
            };

            IEnumerator OnFirstTimeLoaded()
            {
                var blindOperation = FadeBlind(false);
                while (!blindOperation.IsCompleted)
                {
                    yield return null;
                }
                var panelOperation = FadeSetPanelActive(_startMenuPanel, true);
                while (!panelOperation.IsCompleted)
                {
                    yield return null;
                }
            }
        }

        // This is for the New Game button in StartMenu
        public async void NewGame()
        {
            await FadeSetPanelActive(_startMenuPanel, false);
            await FadeBlind(true);
            await FadeSetPanelActive(_loadingPanel, true);
            await GameManager.Instance.StartNewGame();
            await FadeSetPanelActive(_loadingPanel, false);
            await FadeBlind(false);
        }

        // This is for starting a new game in Gameplay, e.g. pause, win, and lose panels
        // If called in pause menu, there is no need to move back the blocks as they are already in place
        public async void RestartGame()
        {
            bool shouldMoveBlocks = _currentActivePanel != _pausePanel;
            await FadeSetPanelActive(_currentActivePanel, false);
            GameManager.Instance.RestartGame(shouldMoveBlocks);
        }

        // Called by the Resume Button in the pause panel
        public async void ResumeGame()
        {
            await FadeSetPanelActive(_currentActivePanel, false);
            GameManager.Instance.ResumeGame();
        }

        // Called by the Back Buttons in pause, win, and lose panels
        public async void LeaveGame()
        {
            await FadeSetPanelActive(_currentActivePanel, false);
            await FadeBlind(true);
            await FadeSetPanelActive(_loadingPanel, true);
            await GameManager.Instance.BackToMenu();
            await FadeSetPanelActive(_loadingPanel, false);
            await FadeBlind(false);
            await FadeSetPanelActive(_startMenuPanel, true);
        }

        // Used only for when Esc key is down when in pause panel
        public async void OnGameResumed()
        {
            await FadeSetPanelActive(_pausePanel, false);
        }

        public async void OnGamePaused()
        {
            await FadeSetPanelActive(_pausePanel, true);
        }

        public async void OnPostGameExit(bool won)
        {
            if (won)
            {
                await FadeSetPanelActive(_winPanel, true);
            }
            else
            {
                await FadeSetPanelActive(_losePanel, true);
            }
        }

        // Called by the Settings Button in StartMenu panel, Back Button in the Settings panel
        public async void FadeSwitchPanel(GameObject target)
        {
            var currentCanvasGroup = _currentActivePanel.GetComponent<CanvasGroup>();
            var targetCanvasGroup = target.GetComponent<CanvasGroup>();

            await _panelFadeOutAnim.PerformAsync(
                currentCanvasGroup,
                () => {
                    currentCanvasGroup.interactable = false;
                    currentCanvasGroup.blocksRaycasts = false;
                },
                null,
                () => _currentActivePanel.SetActive(false)
            );

            await _panelFadeInAnim.PerformAsync(
                targetCanvasGroup,
                () => target.SetActive(true),
                null,
                () => {
                    targetCanvasGroup.interactable = true;
                    targetCanvasGroup.blocksRaycasts = true;
                    _currentActivePanel = target;
                }
            );
        }

        private async Task FadeSetPanelActive(GameObject panel, bool toFadeIn)
        {
            var currentCanvasGroup = panel.GetComponent<CanvasGroup>();

            if (toFadeIn)
            {
                await _panelFadeInAnim.PerformAsync(
                    currentCanvasGroup,
                    () => panel.SetActive(true),
                    null,
                    () => {
                        currentCanvasGroup.interactable = true;
                        currentCanvasGroup.blocksRaycasts = true;
                        _currentActivePanel = panel;
                    }
                );
            }
            else
            {
                await _panelFadeOutAnim.PerformAsync(
                    currentCanvasGroup,
                    () => {
                        currentCanvasGroup.interactable = false;
                        currentCanvasGroup.blocksRaycasts = false;
                    },
                    null,
                    () => {
                        panel.SetActive(false);
                        _currentActivePanel = null;
                    }
                );
            }
        }

        private async Task FadeBlind(bool toFadeIn)
        {
            if (toFadeIn)
            {
                await _blindFadeInAnim.PerformAsync(_blind, () => _blind.blocksRaycasts = true);
            }
            else
            {
                await _blindFadeOutAnim.PerformAsync(
                    _blind,
                    null, // onEnter
                    null, // onPeak
                    () => { //onExit
                        _blind.blocksRaycasts = false;
                        FadeBlindOutComplete.Raise();
                    }
                );
            }
        }
    }
}
