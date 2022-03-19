using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Minesweeper
{
    [ExecuteInEditMode]
    public class MaterialTestController : MonoBehaviour
    {
#if UNITY_EDITOR
        [FormerlySerializedAs("_zero")] [SerializeField] private List<MeshRenderer> _zeros;
        [FormerlySerializedAs("_one")] [SerializeField] private List<MeshRenderer> _ones;
        [FormerlySerializedAs("_two")] [SerializeField] private List<MeshRenderer> _twos;
        [FormerlySerializedAs("_three")] [SerializeField] private List<MeshRenderer> _threes;
        [FormerlySerializedAs("_four")] [SerializeField] private List<MeshRenderer> _fours;
        [FormerlySerializedAs("_five")] [SerializeField] private List<MeshRenderer> _fives;
        [FormerlySerializedAs("_six")] [SerializeField] private List<MeshRenderer> _sixs;
        [FormerlySerializedAs("_seven")] [SerializeField] private List<MeshRenderer> _sevens;
        [FormerlySerializedAs("_eight")] [SerializeField] private List<MeshRenderer> _eights;
        [SerializeField] private List<MeshRenderer> _untouched;
        [SerializeField] private List<MeshRenderer> _marked;
        [SerializeField] private List<MeshRenderer> _mine;

        [SerializeField] private Material _dugBase;
        [SerializeField] private Material _dugEdge;
        [SerializeField] private Material _untouchedBase;
        [SerializeField] private Material _untouchedEdge;
        [SerializeField] private Material _markedBase;
        [SerializeField] private Material _markedEdge;
        [SerializeField] private Material _mineBase;
        [SerializeField] private Material _mineEdge;
        [SerializeField] private Material _one;
        [SerializeField] private Material _two;
        [SerializeField] private Material _three;
        [SerializeField] private Material _four;
        [SerializeField] private Material _five;
        [SerializeField] private Material _six;
        [SerializeField] private Material _seven;
        [SerializeField] private Material _eight;
        
        public void UpdateMaterial()
        {
            foreach (var meshRenderer in _zeros)
            {
                var newMats = new [] {_dugEdge,_dugBase };
                meshRenderer.materials = newMats;
            }
            
            foreach (var meshRenderer in _ones)
            {
                var newMats = new [] {_dugEdge, _dugBase, _one};
                meshRenderer.materials = newMats;
            }

            foreach (var meshRenderer in _twos)
            {
                var newMats = new[] {_dugEdge, _dugBase, _two};
                meshRenderer.materials = newMats;
            }

            foreach (var meshRenderer in _threes)
            {
                var newMats = new [] {_dugEdge, _dugBase, _three};
                meshRenderer.materials = newMats;
            }
            
            foreach (var meshRenderer in _fours)
            {
                var newMats = new [] {_dugEdge, _dugBase, _four};
                meshRenderer.materials = newMats;
            }
            
            foreach (var meshRenderer in _fives)
            {
                var newMats = new [] {_dugEdge, _dugBase, _five};
                meshRenderer.materials = newMats;
            }
            
            foreach (var meshRenderer in _sixs)
            {
                var newMats = new [] {_dugEdge, _dugBase, _six};
                meshRenderer.materials = newMats;
            }
            
            foreach (var meshRenderer in _sevens)
            {
                var newMats = new [] {_dugEdge, _dugBase, _seven};
                meshRenderer.materials = newMats;
            }
            
            foreach (var meshRenderer in _eights)
            {
                var newMats = new [] {_dugEdge, _dugBase, _eight};
                meshRenderer.materials = newMats;
            }

            foreach (var meshRenderer in _untouched)
            {
                var newMats = new[] {_untouchedEdge, _untouchedBase};
                meshRenderer.materials = newMats;
            }

            foreach (var meshRenderer in _marked)
            {
                var newMats = new [] {_markedEdge, _markedBase};
                meshRenderer.materials = newMats;
            }

            foreach (var meshRenderer in _mine)
            {
                var newMats = new[] {_mineEdge, _mineBase};
                meshRenderer.materials = newMats;
            }
        }
#endif
    }
}
