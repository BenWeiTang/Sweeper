using UnityEngine;
using UnityEngine.UI;
using Minesweeper.Reference;

namespace Minesweeper.UI.PanelController
{
    public class LoadingPanelController : APanelController
    {
        public override PanelType Type => PanelType.Loading;

        [SerializeField] private Slider _slider;
        [SerializeField] private FloatRef _progress;

        private void OnDisable()
        {
            _slider.value = 0f;
            _progress.value = 0f;
        }

        private void Update()
        {
            _slider.value = Mathf.MoveTowards(_slider.value, _progress.value, 0.2f * Time.deltaTime);
        }
    }
}
