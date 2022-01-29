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
        [SerializeField] protected float _animDuration = 0.2f;

        protected virtual void OnMouseDown()
        {
            transform.DOScale(Vector3.one * _downScale, _animDuration);
            _uiSpotDown.Raise();
        }
        protected virtual void OnMouseUp()
        {
            transform.DOScale(Vector3.one, _animDuration);
            _uiSpotUp.Raise();
        }
    }
}
