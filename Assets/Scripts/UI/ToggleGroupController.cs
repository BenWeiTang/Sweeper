using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Minesweeper.UI
{
    public class ToggleGroupController : MonoBehaviour
    {
        public event Action<ToggleController> ToggleSelected;
        public int CurrentID => CurrentToggleController.ID;
        
        private ToggleController CurrentToggleController { get; set; }
        private List<ToggleController> _controllers = new List<ToggleController>();

        public void Select(ToggleController toggleController) => ToggleSelected?.Invoke(toggleController);

        public void UpdateCurrentController(int id)
        {
            if (_controllers.All(c => c.ID != id))
            {
                Debug.LogWarning($"No toggle controller has id {id}");
                return;
            }

            var target = _controllers.First(c => c.ID == id);
            target.Select();
        }
        
        private void Awake()
        {
            ToggleSelected += OnSelected;
            _controllers = GetComponentsInChildren<ToggleController>().ToList();
            // Grab the one that is manually set to active in the inspector as the current active toggle controller
            CurrentToggleController = _controllers.First(c => c.IsOn);
        }

        private void OnSelected(ToggleController toggleController)
        {
            CurrentToggleController = toggleController;
        }
    }
}