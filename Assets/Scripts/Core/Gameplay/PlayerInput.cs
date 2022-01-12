using UnityEngine;

namespace Minesweeper.Core
{
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        private ISpot _currentISpot;
        private float _lastMouseDownTime;
        private float _lastDoubleDownUpTime;

        private void Update()
        {
            if (GameManager.Instance.CurrentState == GameState.InGame)
                CheckMouseInput();

            CheckPause();
        }

        private void CheckMouseInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                LeftClick();

                if (Input.GetMouseButton(1))
                {
                    DoubleDown();
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                RightClick();

                if (Input.GetMouseButton(0))
                {
                    DoubleDown();
                }
            }
        }

        private void LeftClick()
        {
            UpdateCurrentISpot();
            _currentISpot?.Dig();
        }

        private void RightClick()
        {
            UpdateCurrentISpot();
            _currentISpot?.Mark();
        }

        private void DoubleDown()
        {
            UpdateCurrentISpot();
            _currentISpot?.ClearNear();
        }

        private void UpdateCurrentISpot()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                _currentISpot = hitInfo.transform.GetComponent<ISpot>();
            }
        }

        private void CheckPause()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameManager.Instance.CurrentState == GameState.InGame)
                    GameManager.Instance.PauseGame();
                else if (GameManager.Instance.CurrentState == GameState.Pause)
                    GameManager.Instance.ResumeGame();
            }
        }
    }
}
