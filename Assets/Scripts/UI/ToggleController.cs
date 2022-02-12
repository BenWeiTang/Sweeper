using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Minesweeper.UI
{
    public class ToggleController : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _image;
        [SerializeField] private ToggleGroupController _groupController;
        private float _defaultAlpha;
        
        private void Awake()
        {
            _defaultAlpha = _image.color.a;
            _image.alphaHitTestMinimumThreshold = 0f;
        }

        private void OnEnable()
        {
            _groupController.ToggleSelected += ToggleSelected;
        }

        private void OnDisable()
        {
            _groupController.ToggleSelected -= ToggleSelected;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Select();
        }

        private void Select() => _groupController.Select(this);

        private void ToggleSelected(ToggleController toggleController)
        {
            if (toggleController == this)
            {
                UpdateAlpha(_image, 1f);
            }
            else
            {
                UpdateAlpha(_image, _defaultAlpha);
            }
        }

        private static void UpdateAlpha(Image image, float alpha)
        {
            alpha = Mathf.Clamp01(alpha);
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
        }
    }
}
