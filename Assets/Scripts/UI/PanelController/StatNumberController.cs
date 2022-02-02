using UnityEngine;
using UnityEngine.EventSystems;
using Minesweeper.Animation;

namespace Minesweeper.UI.PanelController
{
    public class StatNumberController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private CanvasGroup _statNumBlock;
        [SerializeField] private CanvasGroupFade _fadeInAnim;
        [SerializeField] private CanvasGroupFade _fadeOutAnim;

        public async void OnPointerEnter(PointerEventData eventData)
        {
            await _fadeInAnim.PerformAsync(_statNumBlock);
        }

        public async void OnPointerExit(PointerEventData eventData)
        {
            await _fadeOutAnim.PerformAsync(_statNumBlock);
        }
    }
}
