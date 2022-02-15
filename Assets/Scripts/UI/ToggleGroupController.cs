using System;
using System.Collections.Generic;
using System.Linq;
using Minesweeper.Saving;
using UnityEngine;

namespace Minesweeper.UI
{
    public class ToggleGroupController : MonoBehaviour
    {
        [SerializeField] private List<ToggleController> _controllers;

        public event Action<ToggleController> ToggleSelected;
        public int CurrentID => CurrentToggleController.ID;
        public void Select(ToggleController toggleController) => ToggleSelected?.Invoke(toggleController);

        private ToggleController CurrentToggleController { get; set; }

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
        }

        private void Start()
        {
            LoadFromGameSettings();
        }

        private void OnSelected(ToggleController toggleController)
        {
            CurrentToggleController = toggleController;
        }

        // Needs to be called after Awake because the Toggle Controllers need to register to the event ToggleSelected
        // Otherwise, when calling Select() at the end of this method, nothing will happen
        private void LoadFromGameSettings()
        {
            var targetID = SettingsSerializer.LoadSettings().GeneralSettingsData.Difficulty switch
            {
                0 => 0,
                1 => 1,
                2 => 2,
                _ => 0,
            };
            CurrentToggleController = _controllers.First(c => c.ID == targetID);
            Select(CurrentToggleController);
        }
    }
}