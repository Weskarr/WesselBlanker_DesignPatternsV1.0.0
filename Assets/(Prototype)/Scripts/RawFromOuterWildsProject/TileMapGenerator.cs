using System.Collections.Generic;
using UnityEngine;
using System;

namespace TileSystem
{
    public class TileMapGenerator : MonoBehaviour
    {
        [SerializeField] private TileManager tileManager;
        [SerializeField] private MaterialBoard materialBoard;

        [SerializeField] private string _tileMapName;
        [SerializeField] private Mesh _tileMesh;
        [SerializeField] private GameObject _tileMapsParent;

        public void NewTileMap(float tileSize, Vector2Int tileMapSize)
        {
            // Optional: Disable all Children of the TileMapsParent.
            foreach(Transform child in _tileMapsParent.transform)
                child.gameObject.SetActive(false);

            // Step 0: New Local Variables.
            TileMap newTileMap;
            Dictionary<Material, List<Matrix4x4>> newBatchedMatrices;
            Dictionary<Vector2Int, TileData> newTileDictionary;

            // Step 1: Create new Object with the TileMap Component.
            newTileMap = CreateTileMapObject();

            // Step 2: Initialize the Batched Matrices.
            newBatchedMatrices = CreateBatchedMatrices();

            // Step 3: Generate TileData for each Tile within the TileMapSize.
            newTileDictionary = CreateTileDictionary(tileMapSize);

            // Step 4: Assign the TileNeighbors of each Tile.
            newTileDictionary = SetTileNeighborsForAllTiles(newTileDictionary);

            // Step 5: Bake Tile Instances.
            newBatchedMatrices = BakeTileInstances(newTileDictionary, newBatchedMatrices, tileSize);

            // Step 6: Parent the new TileMap.
            newTileMap.transform.parent = _tileMapsParent.transform;

            // Step 7: Assign Local Variables to the new TileMap.
            newTileMap.BatchedMatrices = newBatchedMatrices;
            newTileMap.TileDictionary = newTileDictionary;
            Debug.Log($"Pre-BatchedMatrices Count: {newTileMap.BatchedMatrices.Count}");
            Debug.Log($"Pre-TileDictionary Count: {newTileMap.TileDictionary.Count}");

            // Step 8: Assign Rendering Variables to the new TileMap.
            newTileMap.TileMesh = _tileMesh;
        }

        private TileMap CreateTileMapObject()
        {
            GameObject tileMapObject = new(_tileMapName);
            TileMap tileMap = tileMapObject.AddComponent<TileMap>();
            return tileMap;
        }

        private Dictionary<Material, List<Matrix4x4>> CreateBatchedMatrices()
        {
            Dictionary<Material, List<Matrix4x4>> batchedMatrices = new();

            // Matrix lists
            foreach (var item in materialBoard.MaterialMap)
            {
                Material material = item.Value;
                batchedMatrices[material] = new List<Matrix4x4>();
            }

            return batchedMatrices;
        }

        private Dictionary<Vector2Int, TileData> CreateTileDictionary(Vector2Int tileMapSize)
        {
            Dictionary<Vector2Int, TileData> temporaryTileDictionary = new();

            for (int x = 0; x < tileMapSize.x; x++)
            {
                for (int y = 0; y < tileMapSize.y; y++)
                {
                    Vector2Int gridPos = new(x, y);

                    TileData newTile = BuildGridTileData
                    (
                        materialBoard.GetRandomMaterial(),
                        null, // No GameObject
                        null, // No Renderer
                        FactionEnum.None,
                        gridPos,
                        100f
                    );

                    temporaryTileDictionary[gridPos] = newTile;
                }
            }

            return temporaryTileDictionary;
        }

        private Dictionary<Vector2Int, TileData> SetTileNeighborsForAllTiles(Dictionary<Vector2Int, TileData> tileDictionary)
        {
            Dictionary<Vector2Int, TileData> temporaryTileDictionary = tileDictionary;

            foreach (var kvp in temporaryTileDictionary)
            {
                Vector2Int currentPos = kvp.Key;
                TileData tile = kvp.Value;

                foreach (var directionOffset in tileManager.RelayFourDirectionsDictionary())
                {
                    FourDirectionsEnum direction = directionOffset.Key;
                    Vector2Int offsetAmount = directionOffset.Value;

                    Vector2Int neighborPos = currentPos + offsetAmount;
                    if (temporaryTileDictionary.TryGetValue(neighborPos, out var neighborTileData))
                    {
                        tile.AddNeighbor((int)direction, neighborTileData);
                    }
                }
            }

            return temporaryTileDictionary;
        }

        private Dictionary<Material, List<Matrix4x4>> BakeTileInstances
        (
            Dictionary<Vector2Int, TileData> tileDictionary,
            Dictionary<Material, List<Matrix4x4>> batchedMatrices,
            float tileSize
        )
        {
            Dictionary<Material, List<Matrix4x4>> temporaryBatchedMatrices = batchedMatrices;

            // Clear previous
            foreach (var list in temporaryBatchedMatrices.Values)
                list.Clear();

            foreach (var tile in tileDictionary.Values)
            {
                Vector3 worldPos = ConvertToWorldPosition(tile.GridPosition, tileSize);
                Matrix4x4 matrix = Matrix4x4.TRS(worldPos, Quaternion.identity, new Vector3(tileSize, 0, tileSize));
                temporaryBatchedMatrices[tile.TileMaterial].Add(matrix);
            }

            return temporaryBatchedMatrices;
        }

        private TileData BuildGridTileData
        (
            Material tileMaterial,
            GameObject blockGameObject,
            Renderer blockRenderer,
            FactionEnum factionOwner,
            Vector2Int gridPosition,
            float blockHealthCur
        )
        {
            TileData tileData = new TileData();
            tileData.TileMaterial = tileMaterial;
            tileData.TileGameObject = blockGameObject;
            tileData.TileRenderer = blockRenderer;
            tileData.FactionOwner = factionOwner;
            tileData.GridPosition = gridPosition;
            tileData.TileHealthCur = blockHealthCur;
            return tileData;
        }


        Material GetMaterialForType(FactionEnum type)
        {
            return type switch
            {
                //FactionEnum.None => materialBoard.GetMaterial(MaterialSerieEnum.Mechanical, MaterialTypeEnum.Dark),
                //FactionEnum.Verdancy => materialBoard.GetMaterial(MaterialSerieEnum.Verdancy, MaterialTypeEnum.Middle),
                //FactionEnum.Blight => materialBoard.GetMaterial(MaterialSerieEnum.Blight, MaterialTypeEnum.Middle),
                //FactionEnum.Contested => materialBoard.GetMaterial(MaterialSerieEnum.Water, MaterialTypeEnum.Middle),
                // FactionEnum.Unclaimable => materialBoard.GetMaterial(MaterialSerieEnum.Fire, MaterialTypeEnum.Middle),
                _ => materialBoard.MaterialMissing
            };
        }

        public void ChangeTileType(Vector2Int pos, FactionEnum newType)
        {
            //if (!gridTiles.ContainsKey(pos)) return;
            //gridTiles[pos].FactionOwner = newType;
            //BakeTileInstances(); // Refresh visuals (can be optimized to only update dirty tiles)
        }

        private void SubscribeToOwnershipChanges()
        {
            //foreach (var kvp in gridTiles)
            //{
            //    TileData gridTileData = kvp.Value;
            //
            //    gridTileData.OnChangedFactionOwner += ResultToOwnershipChanges;
            //}
        }

        private void ResultToOwnershipChanges(TileData gridTileData, FactionEnum newFaction)
        {
            //Debug.Log($"{gridTileData.TileGameObject.name} has a new faction owner: {newFaction}");

            foreach (var neighbor in gridTileData.Neighbors)
            {
                //if (neighbor != null)
                //Debug.Log($"{neighbor.TileGameObject.name} I am a neighbor!");
                //else
                //Debug.Log($"No neighbor here..");
            }
        }



        public Vector3 ConvertToWorldPosition(Vector2Int gridPosition, float tileSize)
        {
            return new Vector3(gridPosition.x * tileSize, 0, gridPosition.y * tileSize);
        }

        public Vector2Int ConvertToGridPosition(Vector3 position, float tileSize)
        {
            Vector2Int gridPosition = new
            (
                Mathf.RoundToInt(position.x / tileSize),
                Mathf.RoundToInt(position.z / tileSize)
            );
            return gridPosition;
        }

        public TileData GetBlockDataAtGridPosition(Vector2Int pos)
        {
            //gridTiles.TryGetValue(pos, out TileData blockData);
            //return blockData;
            return null;
        }

        public void RenameBlock(TileData blockData, Vector2Int gridPosition)
        {
            blockData.TileGameObject.name = $"GridBlock_{gridPosition}";
        }

        public void AssignNewMaterial(TileData gridTileData, Material newMaterial)
        {
            Renderer renderer = gridTileData.TileRenderer;
            Material preMaterial = renderer.material;

            if (preMaterial == newMaterial)
                return;

            renderer.sharedMaterial = newMaterial;
        }

        public void StartWithTileMaterial(TileData gridTileData)
        {
            Material newMaterial;
            newMaterial = gridTileData.FactionOwner switch
            {
                //FactionEnum.None => materialBoard.GetMaterial(MaterialSerieEnum.Earth, MaterialTypeEnum.Middle),
                //FactionEnum.Unclaimable => materialBoard.GetMaterial(MaterialSerieEnum.Earth, MaterialTypeEnum.Dark),
                //FactionEnum.Blight => materialBoard.GetMaterial(MaterialSerieEnum.Blight, MaterialTypeEnum.Middle),
                //FactionEnum.Verdancy => materialBoard.GetMaterial(MaterialSerieEnum.Verdancy, MaterialTypeEnum.Middle),
                _ => materialBoard.MaterialMissing
            };

            AssignNewMaterial(gridTileData, newMaterial);
        }

        public void DefaultMaterials()
        {
            //foreach (var kvp in gridTiles)
            //{
            //    TileData tempData = kvp.Value;
            //    StartWithTileMaterial(tempData);
            //}
        }

        public void OccupiedMaterials()
        {
            //foreach (var kvp in gridTiles)
            {
                //Material newMaterial;
                //TileData tempData = kvp.Value;
                //if (tempData.PlacementData != null)
                {
                    //newMaterial = tempData.PlacementData.GrowthTier switch
                    //{
                    //   WildGrowthTierEnum.UnderGrowth => materialBoard.GetMaterial(MaterialSerieEnum.Fire, MaterialTypeEnum.Outline),
                    //    WildGrowthTierEnum.LowGrowth => materialBoard.GetMaterial(MaterialSerieEnum.Fire, MaterialTypeEnum.Dark),
                    //   WildGrowthTierEnum.MiddleGrowth => materialBoard.GetMaterial(MaterialSerieEnum.Fire, MaterialTypeEnum.Middle),
                    //    WildGrowthTierEnum.HighGrowth => materialBoard.GetMaterial(MaterialSerieEnum.Fire, MaterialTypeEnum.Light),
                    //    _ => materialBoard.MaterialMissing
                    //};
                }
                //else
                // newMaterial = materialBoard.GetMaterial(MaterialSerieEnum.Water, MaterialTypeEnum.Middle);

                //tempData.TileRenderer.sharedMaterial = newMaterial;
            }
        }

        public HashSet<Vector2Int> GetGridAreaPositions(Vector2Int centerPosition, int areaRange)
        {
            HashSet<Vector2Int> areaPositions = new HashSet<Vector2Int>();
            for (int x = -areaRange; x <= areaRange; x++)
            {
                for (int y = -areaRange; y <= areaRange; y++)
                {
                    Vector2Int tilePos = centerPosition + new Vector2Int(x, y);
                    areaPositions.Add(tilePos);
                }
            }
            //Debug.Log($"Grid Area is: {areaPositions.Count} + {areaRange}");
            return areaPositions;
        }



        public PlacementData BuildPlacementData
        (
        //GameObject placementObject,
        //WildGrowthTierEnum growthTier
        )
        {
            PlacementData placementData = new()
            {
                // PlacementObject = placementObject,
                //   GrowthTier = growthTier
            };
            return placementData;
        }
    }
}