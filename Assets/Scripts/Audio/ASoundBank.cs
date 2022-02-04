using System;
using System.Collections.Generic;
using UnityEngine;

namespace Minesweeper.Audio
{
    public abstract class ASoundBank<T> : ScriptableObject
    {
        [SerializeField] protected List<TrackType> _tracks;
        protected Dictionary<T, Track> _playlist = new Dictionary<T, Track>();

        public virtual void UpdatePlaylist()
        {
            if (_tracks == null)
                return;
             
            _playlist.Clear();
            foreach(var track in _tracks)
            {
                _playlist[track.type] = track.track;
            }
        }

        public virtual Track GetTrack(T type) 
        {
            if (!_playlist.ContainsKey(type))
                UpdatePlaylist();

            return _playlist[type];
        }


        [Serializable]
        public struct TrackType
        {
            public Track track;
            public T type;
        }
    }
}