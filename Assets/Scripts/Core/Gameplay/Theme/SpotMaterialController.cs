using System.Collections.Generic;
using UnityEngine;

namespace Minesweeper.Core
{
    public class SpotMaterialController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _numBlocks;

        [Header("Special Blocks")]
        [SerializeField] private GameObject _untouchedBlock;
        [SerializeField] private GameObject _markedBlock;
        [SerializeField] private GameObject _mineBlock;

        private Theme _theme;

        private void Awake()
        {
            _theme = SettingsManager.Instance.CurrentTheme;
            UpdateMaterials();
        }

        private void UpdateMaterials()
        {
            // At index 0 -> edge material
            // At index 1 -> base material
            // At index 2 -> number material (if applicable)

            // Setting the number blocks, which uses dug mats
            for (int i = 0; i < _numBlocks.Count; i++)
            {
                // We need to assign this entire newly created array to MeshRenderer.materials
                // When doing so, we have to call the GetComponent function;
                // Caching a reference to that array has not effect because MeshRenderer.materials
                // actually returns a copy
                Material[] newMaterials; 
                
                // For 0, there are only two materials because it is just a blank spot
                if (i == 0)
                {
                    newMaterials = new[] {_theme.DugEdgeMaterial, _theme.DugBaseMaterial};
                    _numBlocks[i].GetComponent<MeshRenderer>().materials = newMaterials;
                    continue;
                }

                newMaterials = new Material[3];
                newMaterials[0] = _theme.DugEdgeMaterial;
                newMaterials[1] = _theme.DugBaseMaterial;

                newMaterials[2] = i switch
                {
                    1 => _theme.OneMaterial,
                    2 => _theme.TwoMaterial,
                    3 => _theme.ThreeMaterial,
                    4 => _theme.FourMaterial,
                    5 => _theme.FiveMaterial,
                    6 => _theme.SixMaterial,
                    7 => _theme.SevenMaterial,
                    8 => _theme.EightMaterial,
                    _ => newMaterials[2]
                };

                _numBlocks[i].GetComponent<MeshRenderer>().materials = newMaterials;
            }

            // Setting special blocks mats
            var newMineMats = new Material[2];
            var newUntouchedMats = new Material[2];
            var newMarkedMats = new Material[2];
            
            newMineMats[0] = _theme.MineEdgeMaterial;
            newMineMats[1] = _theme.MineBaseMaterial;
            newUntouchedMats[0] = _theme.UntouchedEdgeMaterial;
            newUntouchedMats[1] = _theme.UntouchedBaseMaterial;
            newMarkedMats[0] = _theme.MarkedEdgeMaterial;
            newMarkedMats[1] = _theme.MarkedBaseMaterial;

            _mineBlock.GetComponent<MeshRenderer>().materials = newMineMats;
            _untouchedBlock.GetComponent<MeshRenderer>().materials = newUntouchedMats;
            _markedBlock.GetComponent<MeshRenderer>().materials = newMarkedMats;
        }
    }
}