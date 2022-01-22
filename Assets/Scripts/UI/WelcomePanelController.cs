using UnityEngine;
using Minesweeper.Event;

namespace Minesweeper.UI
{
    public class WelcomePanelController : APanelController
    {
        [SerializeField] private VoidEvent _firstClick;
        [SerializeField] private VoidEvent _mouseButtonClick;
        public override PanelType Type => PanelType.Welcome;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
            {
                print("Clicked");
                _firstClick?.Raise();
                _mouseButtonClick?.Raise();
            }
        }

    }
}
