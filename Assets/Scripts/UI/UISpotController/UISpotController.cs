using UnityEngine;
using Minesweeper.Event;
using DG.Tweening;

namespace Minesweeper.UI.SpotController
{
    [RequireComponent(typeof(BoxCollider))]
    public class UISpotController : MonoBehaviour
    {
        [SerializeField] protected VoidEvent _uiSpotDown;
        [SerializeField] protected VoidEvent _uiSpotUp;
        [SerializeField] protected float _downScale = 0.7f;

        protected void OnMouseDown()
        {
            transform.DOScale(Vector3.one * _downScale, 0.5f);
            _uiSpotDown.Raise();
        }
        protected void OnMouseUp()
        {
            transform.DOScale(Vector3.one, 0.5f);
            _uiSpotUp.Raise();
        }
    }
}
