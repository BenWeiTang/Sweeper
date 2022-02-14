using System;
using System.Collections.Generic;
using UnityEngine;

namespace Minesweeper.UI
{
    public class ToggleGroupController : MonoBehaviour
    {
        public event Action<ToggleController> ToggleSelected;

        public void Select(ToggleController toggleController) => ToggleSelected?.Invoke(toggleController);
    }
}