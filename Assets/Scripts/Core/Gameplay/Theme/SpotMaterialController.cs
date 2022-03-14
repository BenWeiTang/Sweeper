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
            
            for (int i = 0; i < _numBlocks.Count; i++)
            {
                var materials = _numBlocks[i].GetComponent<MeshRenderer>().materials;
                materials[0].color = _theme.DugEdge;
                materials[1].color = _theme.DugBase;

                switch (i)
                {
                    case 1:
                        materials[2].color = _theme.One;
                        break;
                    case 2:
                        materials[2].color = _theme.Two;
                        break;
                    case 3:
                        materials[2].color = _theme.Three;
                        break;
                    case 4:
                        materials[2].color = _theme.Four;
                        break;
                    case 5:
                        materials[2].color = _theme.Five;
                        break;
                    case 6:
                        materials[2].color = _theme.Six;
                        break;
                    case 7:
                        materials[2].color = _theme.Seven;
                        break;
                    case 8:
                        materials[2].color = _theme.Eight;
                        break;
                }
            }

            var mineBlockMat = _mineBlock.GetComponent<MeshRenderer>().materials;
            var untouchedBlockMat = _untouchedBlock.GetComponent<MeshRenderer>().materials;
            var markedBlockMat = _markedBlock.GetComponent<MeshRenderer>().materials;

            mineBlockMat[0].color = _theme.MineEdge;
            mineBlockMat[1].color = _theme.MineBase;
            untouchedBlockMat[0].color = _theme.UntouchedEdge;
            untouchedBlockMat[1].color = _theme.UntouchedBase;
            markedBlockMat[0].color = _theme.MarkedEdge;
            markedBlockMat[1].color = _theme.MarkedBase;
        }
    }
}