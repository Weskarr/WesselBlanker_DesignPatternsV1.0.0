using System.Collections.Generic;
using UnityEngine;
using TileSystem;

namespace PreviewSystem
{
    public class PreviewTiles : MonoBehaviour
    {
        [SerializeField] private PreviewObject previewObject;
        [SerializeField] private TileMapGenerator gridTilesManager;

        [SerializeField] private Material canPlaceMat;
        [SerializeField] private Material cannotPlaceMat;
        [SerializeField] private Dictionary<Vector2Int, PreviewTileData> previewTiles = new();

        public void AddChildrenAsPreviewTiles()
        {
            foreach (Transform child in transform)
            {
                Vector3 worldPosition = child.localPosition;
                Vector2Int gridPosition = gridTilesManager.ConvertToGridPosition(worldPosition, 1);
                if (!previewTiles.ContainsKey(gridPosition))
                {
                    PreviewTileData tempData = BuildPreviewTileData
                    (
                         false,
                         child.gameObject,
                         child.GetComponent<Renderer>()
                    );

                    previewTiles.Add(gridPosition, tempData);
                }
            }
        }

        public void UpdateEachPreviewTile(PlacementData currentPlacement)
        {
            if (previewTiles.Count == 0)
            {
                Debug.LogWarning("PreviewTiles is Zero, unable to Update!");
                return;
            }

            Vector3 worldCenterPosition = previewObject.GetCenterPosition();
            Vector2Int gridCenterPosition = gridTilesManager.ConvertToGridPosition(worldCenterPosition, 1);

            foreach (var kvp in previewTiles)
            {
                Vector2Int currentPosition = kvp.Key;
                PreviewTileData currentData = kvp.Value;

                Vector2Int currentGridTilePosition = gridCenterPosition + currentPosition;
                TileData gridTileData = gridTilesManager.GetBlockDataAtGridPosition(currentGridTilePosition);


                if (gridTileData == null ||
                    gridTileData.FactionOwner != FactionEnum.Verdancy ||
                    (gridTileData.PlacementData != null && gridTileData.PlacementData != currentPlacement))
                {
                    currentData.IsPlaceable = false;
                    currentData.Renderer.sharedMaterial = cannotPlaceMat;
                }
                else
                {
                    currentData.IsPlaceable = true;
                    currentData.Renderer.sharedMaterial = canPlaceMat;
                }
            }
        }

        public void PreviewTilesEnabler(bool direction)
        {
            foreach (var kvp in previewTiles)
            {
                Renderer renderer = kvp.Value.Renderer;
                if (renderer.enabled == direction)
                    return;
                renderer.enabled = direction;
            }
        }

        public bool IsPlaceableReturner()
        {
            bool isPlaceable = true;
            foreach (var kvp in previewTiles)
            {
                if (kvp.Value.IsPlaceable)
                    continue;
                else
                    isPlaceable = false;
            }
            return isPlaceable;
        }

        private PreviewTileData BuildPreviewTileData
        (
            bool isPlaceable,
            GameObject gameObject,
            Renderer renderer
        )
        {
            PreviewTileData placementData = new()
            {
                IsPlaceable = isPlaceable,
                GameObject = gameObject,
                Renderer = renderer
            };
            return placementData;
        }
    }
}