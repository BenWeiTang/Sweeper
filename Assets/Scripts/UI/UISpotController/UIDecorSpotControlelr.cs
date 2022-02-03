using System.Collections;
using UnityEngine;
using Minesweeper.Event;


namespace Minesweeper.UI.SpotController
{
    public class UIDecorSpotControlelr : UISpotController
    {
        [SerializeField] private VoidEvent _firstClick;

        public void OnFirstClicked() => _firstClick = null;

        protected override void OnMouseDown()
        {
            base.OnMouseDown();
        }

        protected override void OnMouseUp()
        {
            base.OnMouseUp();
            _firstClick?.Raise();
        }
    }
}
