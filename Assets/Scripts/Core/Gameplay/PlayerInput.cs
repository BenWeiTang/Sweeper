using System;
using UnityEngine;
using Minesweeper.Reference;

namespace Minesweeper.Core
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private ComboController _comboController;
        [SerializeField] private ACEController _ACEController;
        [SerializeField] private BoolRef _inPauseTransition;

        public event Action EndGameLeftClick;
        
        private Ray ScreenPointToRay {get; set;} = new Ray();

        private ISpot _currentISpot;
        private float _lastEscDown;
        private bool _useEasyClear;

        private void Awake()
        {
            _useEasyClear = SettingsManager.Instance.MasterSettingsData.GeneralSettingsData.EasyClear;
        }

        private void Update()
        {
            ScreenPointToRay = _camera.ScreenPointToRay(Input.mousePosition);
            if (GameManager.Instance.CurrentState == GameState.InGame)
                CheckMouseInput();
            else if (GameManager.Instance.CurrentState == GameState.PostGame)
                CheckEndGameLeftClick();

            CheckPause();
        }

        private void CheckMouseInput()
        {
            if (Input.GetMouseButtonDown(0) && Input.GetMouseButtonDown(1))
            {
                DoubleDown(true);
            }
            else if (Input.GetMouseButtonDown(0))
            {
                LeftClick();

                if (Input.GetMouseButton(1) && !_useEasyClear)
                {
                    DoubleDown(false);
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                RightClick();

                if (Input.GetMouseButton(0) && !_useEasyClear)
                {
                    DoubleDown(false);
                }
            }
        }

        // Increment of _click.value has to come before calling _currentISpot.Dig()
        // If Dig() is called before _click.value++, what will happen is that
        // OnFirstSafeSpotDug gets called first, which sets the value to 1
        // and then _clickCount.value gets incremented one more time,
        // resulting in that the first click will be counted as two
        private void LeftClick()
        {
            UpdateCurrentISpot();
            if (_currentISpot != null)
            {
                _ACEController.UpdateClickCount(1);
                _comboController.StartCountingDigs();
                _currentISpot.Dig();

                if (_useEasyClear)
                {
                    // This is stupid but should work
                    // This double down is zero in the delta
                    _ACEController.UpdateClickCount(-1);
                    DoubleDown(true);
                }
            }
        }

        private void RightClick()
        {
            UpdateCurrentISpot();
            if (_currentISpot != null)
            {
                _ACEController.UpdateClickCount(1);
                _currentISpot.Mark();
            }
        }

        // addOne is true when both mouse buttons are pressed during the same frame
        // In such cases, we want to increment click count by one
        // addOne is false when one mouse button is down while the other is already on hold
        // In such cases, we want to decrement click count by one,
        // as there will be an additional call of RightClick() or LeftClick(),
        // either of which increments click count by one, which we don't want
        private void DoubleDown(bool addOne)
        {
            UpdateCurrentISpot();
            if (_currentISpot != null)
            {
                _comboController.StartCountingDigs();
                _currentISpot.ClearNear();
                _ACEController.UpdateClickCount(addOne ? 1 : -1);
            }
        }

        private void UpdateCurrentISpot()
        {
            if (Physics.Raycast(ScreenPointToRay, out RaycastHit hitInfo))
            {
                _currentISpot = hitInfo.transform.GetComponent<ISpot>();
            }
        }

        private void CheckPause()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && Time.time > _lastEscDown + 1f && !_inPauseTransition.value)
            {
                _lastEscDown = Time.time;

                if (GameManager.Instance.CurrentState == GameState.InGame)
                    GameManager.Instance.PauseGame();
                else if (GameManager.Instance.CurrentState == GameState.Pause)
                    GameManager.Instance.ResumeGame();
            }
        }

        private void CheckEndGameLeftClick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                EndGameLeftClick?.Invoke();
            }
        }
    }
}
