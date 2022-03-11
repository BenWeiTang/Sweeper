using System;
using UnityEngine;

namespace Minesweeper.Core
{
    [RequireComponent(typeof(Camera))]
    public class CameraBackgroundColorController : MonoBehaviour
    {
        private Camera _camera;
        private Theme _theme;

        private void Awake()
        {
            _camera = GetComponent<Camera>();
            _theme = SettingsManager.Instance.CurrentTheme;

            _camera.backgroundColor = _theme.BackgroundColor;
        }
    }
}
