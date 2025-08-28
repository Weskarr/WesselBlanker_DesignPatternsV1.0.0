using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TileSystem
{
    public class TileMap : MonoBehaviour
    {
        private Dictionary<Vector2Int, TileData> _tileDictionary = new();
        Dictionary<Material, List<Matrix4x4[]>> _batchedMatrixChunks;
        private Mesh _tileMesh;
        private float _tileSize;

        private const int MaxInstances = 1023;

        private Dictionary<Material, List<Matrix4x4>> _batchedMatrices = new();

        public float TileSize
        {
            get => _tileSize;
            set => _tileSize = value;
        }

        public Dictionary<Material, List<Matrix4x4>> BatchedMatrices
        {
            get => _batchedMatrices;
            set => _batchedMatrices = value;
        }

        public Dictionary<Vector2Int, TileData> TileDictionary
        {
            get => _tileDictionary;
            set => _tileDictionary = value;
        }

        public Dictionary<Material, List<Matrix4x4[]>> BatchedMatrixChunks 
        { 
            get => _batchedMatrixChunks; 
            set => _batchedMatrixChunks = value;
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

            foreach (var (mat, chunks) in _batchedMatrixChunks)
            {
                foreach (var matrixChunk in chunks)
                {
                    Graphics.DrawMeshInstanced(_tileMesh, 0, mat, matrixChunk, matrixChunk.Length);
                }
            }
        }

        public void RefreshTileInstance(TileData tileData, float tileSize)
        {
            Vector2Int gridPos = tileData.GridPosition;
            Material newMaterial = tileData.TileMaterial;

            // Track the old material (if any)
            Material oldMaterial = null;

            // Find and remove the old matrix (and remember its material)
            foreach (var kvp in _batchedMatrices)
            {
                var mat = kvp.Key;
                var list = kvp.Value;

                int removed = list.RemoveAll(m => IsSamePositionMatrix(m, gridPos, tileSize));
                if (removed > 0)
                {
                    oldMaterial = mat;
                    break; // Assume only one material had this instance
                }
            }

            // Add new matrix
            var newMatrix = Matrix4x4.TRS(ConvertToWorldPosition(gridPos, tileSize), Quaternion.identity, new Vector3(tileSize, 0, tileSize));
            if (!_batchedMatrices.ContainsKey(newMaterial))
                _batchedMatrices[newMaterial] = new List<Matrix4x4>();
            _batchedMatrices[newMaterial].Add(newMatrix);

            // Re-chunk only the affected materials
            if (oldMaterial != null)
                _batchedMatrixChunks[oldMaterial] = SplitMaterialIntoChunks(_batchedMatrices[oldMaterial]);

            _batchedMatrixChunks[newMaterial] = SplitMaterialIntoChunks(_batchedMatrices[newMaterial]);
        }

        private List<Matrix4x4[]> SplitMaterialIntoChunks(List<Matrix4x4> fullList)
        {
            var chunks = new List<Matrix4x4[]>();

            int total = fullList.Count;
            for (int i = 0; i < total; i += MaxInstances)
            {
                int count = Mathf.Min(MaxInstances, total - i);
                Matrix4x4[] chunk = new Matrix4x4[count];
                fullList.CopyTo(i, chunk, 0, count);
                chunks.Add(chunk);
            }
            return chunks;
        }

        private bool IsSamePositionMatrix(Matrix4x4 matrix, Vector2Int gridPos, float tileSize)
        {
            Vector3 pos = new(gridPos.x * tileSize, 0, gridPos.y * tileSize);
            return Vector3.Distance(matrix.GetColumn(3), pos) < 0.01f;
        }

        public Vector3 ConvertToWorldPosition(Vector2Int gridPosition, float tileSize)
        {
            return new Vector3(gridPosition.x * tileSize, 0, gridPosition.y * tileSize);
        }

        public void DebugChunkInfo()
        {
            int grandTotal = _batchedMatrixChunks.Values.SelectMany(c => c).Sum(chunk => chunk.Length);
            Debug.Log($"TOTAL Instances across all materials: {grandTotal}");

            foreach (var kvp in _batchedMatrixChunks)
            {
                List<Matrix4x4[]> chunks = kvp.Value;
                Material mat = kvp.Key;

                int total = chunks.Sum(c => c.Length);
                Debug.Log($"Material '{mat.name}' - Total Instances: {total}");

                for (int i = 0; i < chunks.Count; i++)
                {
                    Debug.Log($"Material '{mat.name}' - Chunk {i}: {chunks[i].Length} instances");
                }
            }
        }
    }
}