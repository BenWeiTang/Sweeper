using UnityEngine;
using Minesweeper.Event;


namespace Minesweeper.UI.SpotController
{
    public class UIDecorSpotControlelr : UISpotController
    {
        [SerializeField] private VoidEvent _firstClick;

        protected override void OnMouseDown()
        {
            base.OnMouseDown();
        }

        protected override void OnMouseUp()
        {
            base.OnMouseUp();
            _firstClick.Raise();
        }
    }
}
