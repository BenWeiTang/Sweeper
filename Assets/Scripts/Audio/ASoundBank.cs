using System;
using System.Collections.Generic;
using UnityEngine;

namespace Minesweeper.Audio
{
    public abstract class ASoundBank<T> : ScriptableObject
    {
        [SerializeField] protected List<TrackType> _tracks;
        protected Dictionary<T, Track> _playlist = new Dictionary<T, Track>();

        [Serializable]
        public struct TrackType
        {
            public Track track;
            public T type;
        }
    }
}