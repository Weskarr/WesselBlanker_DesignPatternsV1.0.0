using System.Collections.Generic;
using UnityEngine;

namespace TileSystem
{
    public class TileMap : MonoBehaviour
    {
        [SerializeField] private Dictionary<Vector2Int, TileData> _tileDictionary = new();
        private Dictionary<Material, List<Matrix4x4>> _batchedMatrices = new();

        [SerializeField] private Mesh _tileMesh;
        //[SerializeField] private MaterialBoard _materialBoard;

        public Dictionary<Vector2Int, TileData> TileDictionary
        {
            get => _tileDictionary;
            set => _tileDictionary = value;
        }

        public Dictionary<Material, List<Matrix4x4>> BatchedMatrices
        {
            get => _batchedMatrices;
            set => _batchedMatrices = value;
        }

        public Mesh TileMesh
        {
            get => _tileMesh;
            set => _tileMesh = value;
        }

        private void LateUpdate()
        {
            if (_tileMesh == null)
                return;

            if (_tileMesh.subMeshCount == 1)
                RenderTilesOneMat();
            else
                RenderTilesTwoMat();
        }

        void RenderTilesOneMat()
        {
            foreach (var pair in _batchedMatrices)
            {
                Material mat = pair.Key;
                var matrices = pair.Value;

                for (int i = 0; i < matrices.Count; i += 1023)
                {
                    int count = Mathf.Min(1023, matrices.Count - i);
                    Graphics.DrawMeshInstanced(_tileMesh, 0, mat, matrices.GetRange(i, count));
                }
            }
        }

        void RenderTilesTwoMat()
        {
            foreach (var pair in _batchedMatrices)
            {
                Material mat = pair.Key;
                var matrices = pair.Value;

                for (int i = 0; i < matrices.Count; i += 1023)
                {
                    int count = Mathf.Min(1023, matrices.Count - i);
                    var batch = matrices.GetRange(i, count);

                    // Render submesh 0 with mat1
                    Graphics.DrawMeshInstanced(_tileMesh, 0, mat, batch);

                    // Render submesh 1 with mat2
                    //Graphics.DrawMeshInstanced(_tileMesh, 1, mat, batch);
                }
            }
        }
    }
}