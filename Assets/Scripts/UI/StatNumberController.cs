using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Minesweeper.Animation;

namespace Minesweeper.UI
{
    public class StatNumberController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private CanvasGroup _statNumBlock;
        [SerializeField] private MoveTo _moveUpAnim;
        [SerializeField] private CanvasGroupFade _fadeInAnim;
        [SerializeField] private CanvasGroupFade _fadeOutAnim;

        public async void OnPointerEnter(PointerEventData eventData)
        {
            Vector3 og = _statNumBlock.transform.position;
            _statNumBlock.transform.position -= Vector3.up * 10f; 
            List<Task> tasks = new List<Task>();

            var fadeOperation = _fadeInAnim.PerformAsync(_statNumBlock);
            var moveOperation = _moveUpAnim.PerformAsync(_statNumBlock.transform, og);
            tasks.Add(fadeOperation);
            tasks.Add(moveOperation);

            await Task.WhenAll(tasks);
        }

        public async void OnPointerExit(PointerEventData eventData)
        {
            await _fadeOutAnim.PerformAsync(_statNumBlock);
        }
    }
}
