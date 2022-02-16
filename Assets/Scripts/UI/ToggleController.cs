using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Minesweeper.UI
{
    public class ToggleController : MonoBehaviour, IPointerClickHandler
    {
        public int ID => _id;
        public bool IsOn => _isOn;

        [SerializeField] private int _id;
        [SerializeField] private Image _image;
        [SerializeField] private ToggleGroupController _groupController;
        [SerializeField] private bool _isOn;
        [SerializeField] private UnityEvent _onSelected;
        
        private float _defaultAlpha;
        
        public void Select() => _groupController.Select(this);
        
        private void Awake()
        {
            _defaultAlpha = _image.color.a;
            _image.alphaHitTestMinimumThreshold = 0f;
            UpdateAlpha(_isOn ? 1f : _defaultAlpha);
        }

        private void OnEnable()
        {
            _groupController.ToggleSelected += OnToggleSelected;
        }

        private void OnDisable()
        {
            _groupController.ToggleSelected -= OnToggleSelected;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Select();
            _onSelected?.Invoke();
        }

        private void OnToggleSelected(ToggleController toggleController)
        {
            if (toggleController == this)
            {
                UpdateAlpha(1f);
                _isOn = true;
            }
            else
            {
                UpdateAlpha(_defaultAlpha);
                _isOn = false;
            }
        }

        private void UpdateAlpha(float alpha)
        {
            alpha = Mathf.Clamp01(alpha);
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, alpha);
        }
    }
}
