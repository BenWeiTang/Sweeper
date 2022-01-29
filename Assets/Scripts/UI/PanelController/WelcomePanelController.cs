using System.Collections;
using UnityEngine;
using Minesweeper.Event;

namespace Minesweeper.UI.PanelController
{
    public class WelcomePanelController : APanelController
    {
        [SerializeField] private VoidEvent _firstClick;
        [SerializeField] private VoidEvent _mouseButtonClick;
        public override PanelType Type => PanelType.Welcome;

        private bool _shouldDetectClick = false;
        private bool _hasClicked = false;

        private void Start()
        {
            StartCoroutine(ClickInitDelay());
        }

        private IEnumerator ClickInitDelay()
        {
            yield return new WaitForSeconds(0.5f);
            _shouldDetectClick = true;
        }

        private void Update()
        {
            if (_shouldDetectClick && !_hasClicked && (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2)))
            {
                _firstClick?.Raise();
                _mouseButtonClick?.Raise();
                _hasClicked = true;
            }
        }
    }
}
