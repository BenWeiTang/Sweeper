using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Minesweeper.Event;
using DG.Tweening;

namespace Minesweeper.Core
{
    public class UIManager : AManager<UIManager>
    {
        [Header("Event")]
        [SerializeField] private VoidEvent FadeBlindOutComplete;

        [Header("Fade Blind")]
        [SerializeField] private CanvasGroup _blind;
        [SerializeField, Range(0.1f, 5f)] private float _blindFadeDuration = 1f;

        [Header("Panels")]
        [SerializeField, Range(0.1f, 5f)] private float _panelFadeDuration = 1f;
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
        public async void RestartGame()
        {
            await FadeSetPanelActive(_currentActivePanel, false);
            // await FadeBlind(true);
            // await FadeSetPanelActive(_loadingPanel, true);
            GameManager.Instance.RestartGame();
            // await FadeSetPanelActive(_loadingPanel, false);
            // await FadeBlind(false);
        }

        // Called by the Resume Button in the pause panel
        public async void ResumeGame()
        {
            await FadeSetPanelActive(_currentActivePanel, false);
            await FadeBlind(true);
            GameManager.Instance.ResumeGame();
            await FadeSetPanelActive(_loadingPanel, false);
            await FadeBlind(false);
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

        public async void OnGamePaused()
        {
            await FadeSetPanelActive(_pausePanel, true);
        }

        public async void OnGameResumed()
        {
            await FadeSetPanelActive(_pausePanel, false);
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

            currentCanvasGroup.interactable = false;
            currentCanvasGroup.blocksRaycasts = false;
            await Fade(currentCanvasGroup, false); // its alpha goes to 0
            _currentActivePanel.SetActive(false);

            target.SetActive(true);
            await Fade(targetCanvasGroup, true);
            targetCanvasGroup.interactable = true;
            targetCanvasGroup.blocksRaycasts = true;
            _currentActivePanel = target;
        }

        private async Task FadeSetPanelActive(GameObject panel, bool toActivate)
        {
            var currentCanvasGroup = panel.GetComponent<CanvasGroup>();

            if (toActivate)
            {
                panel.SetActive(true);
                await Fade(currentCanvasGroup, true);
                currentCanvasGroup.interactable = true;
                currentCanvasGroup.blocksRaycasts = true;
                _currentActivePanel = panel;
            }
            else
            {
                currentCanvasGroup.interactable = false;
                currentCanvasGroup.blocksRaycasts = false;
                await Fade(currentCanvasGroup, false);
                panel.SetActive(false);
                _currentActivePanel = null;
            }
        }

        private async Task Fade(CanvasGroup cg, bool toTurnOn)
        {
            if (toTurnOn)
            {
                var operation = cg.DOFade(1f, _panelFadeDuration).SetEase(Ease.OutQuad).AsyncWaitForCompletion();
                while (!operation.IsCompleted)
                {
                    await Task.Yield();
                }
            }
            else
            {
                var operation = cg.DOFade(0f, _panelFadeDuration).SetEase(Ease.OutQuad).AsyncWaitForCompletion();
                while (!operation.IsCompleted)
                {
                    await Task.Yield();
                }
            }
        }

        private async Task FadeBlind(bool toFadeIn)
        {
            if (toFadeIn)
            {
                _blind.blocksRaycasts = true;
                var operation = _blind.DOFade(1f, _blindFadeDuration).SetEase(Ease.OutQuad).AsyncWaitForCompletion();
                while (!operation.IsCompleted)
                {
                    await Task.Yield();
                }
            }
            else
            {
                var operation = _blind.DOFade(0f, _blindFadeDuration).SetEase(Ease.OutQuad).AsyncWaitForCompletion();
                while (!operation.IsCompleted)
                {
                    await Task.Yield();
                }
                _blind.blocksRaycasts = false;
                FadeBlindOutComplete.Raise();
            }
        }
    }
}
