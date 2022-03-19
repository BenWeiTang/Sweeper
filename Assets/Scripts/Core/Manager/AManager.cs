using System.Runtime.CompilerServices;
using UnityEngine;

namespace Minesweeper.Core
{
    public abstract class AManager<T> : MonoBehaviour where T : AManager<T>
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectsOfType<T>() as T;

                    if (_instance == null)
                    {
                        Debug.LogError($"Cannont find an instance of {typeof(T)}. Please Manually place it in scene.");
                    }
                }
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
            }
        }

        private void OnApplicationQuit() 
        {
            _instance = null;     
        }
    }
}