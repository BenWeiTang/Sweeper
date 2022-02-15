using System;
using System.Collections.Generic;
using System.Linq;
using Minesweeper.Saving;
using UnityEngine;

namespace Minesweeper.UI
{
    public class ToggleGroupController : MonoBehaviour
    {
        public event Action<ToggleController> ToggleSelected;
        public int CurrentID => CurrentToggleController.ID;

        private ToggleController CurrentToggleController { get; set; }
        [SerializeField] private List<ToggleController> _controllers;

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
        }

        private void Start()
        {
            var targetID = SettingsSerializer.LoadSettings().GeneralSettingsData.Difficulty switch
            {
                0 => 0,
                1 => 1,
                2 => 2,
                _ => 0,
            };
            CurrentToggleController = _controllers.First(c => c.ID == targetID);
            Debug.Log(CurrentToggleController.ID);
            Select(CurrentToggleController);
        }

        private void OnSelected(ToggleController toggleController)
        {
            CurrentToggleController = toggleController;
        }
    }
}