using System.Collections.Generic;
using TileSystem;
using UnityEngine;

namespace PreviewSystem
{
    public class PlacementPreviewer : MonoBehaviour
    {
        //[SerializeField] private FactionTerritoryV2 belongsToFaction;
        [SerializeField] private TileMapGenerator gridTilesManager;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private float tileSize = 0.4f;
        [SerializeField] private float rayMaxDistance = 40f;

        [SerializeField] private Vector3 previewOffset;
        [SerializeField] private Vector3 previewPosition;
        [SerializeField] private PlacementData selectedPlacement;

        [SerializeField] private PreviewObject previewObject3x3Plant;

        private void Start()
        {
            mainCamera = Camera.main;
            previewObject3x3Plant.StartPreview();
            previewObject3x3Plant.PreviewObjectEnabler(false);
        }

        void Update()
        {
            if (selectedPlacement == null)
            {
                if (Input.GetMouseButton(0))
                    CollectingPlacements();

                if (Input.GetMouseButtonDown(0))
                    ClickedPlacement();
            }
            else
            {
                if (Input.GetMouseButtonDown(1))
                    CancelPlacement();

                if (Input.GetMouseButtonDown(0))
                    AttemptPlacement();

                MovePreview();
                previewObject3x3Plant.UpdatePreview(selectedPlacement);
            }
        }

        private void MovePreview()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, rayMaxDistance, groundLayer))
            {
                previewPosition = GetSnappedPosition(hit.point, previewOffset);
                MoveToNewPosition(previewPosition);
            }
        }

        private void CollectingPlacements()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, rayMaxDistance, groundLayer))
            {
                Vector2Int hitGridPosition = gridTilesManager.ConvertToGridPosition(hit.point, 1);
                TileData hitTileData = gridTilesManager.GetBlockDataAtGridPosition(hitGridPosition);
                if (hitTileData != null)
                {
                    PlacementData hitPlacementData = hitTileData.PlacementData;
                }
            }
        }

        private void CancelPlacement()
        {
            previewObject3x3Plant.PreviewObjectEnabler(false);
            selectedPlacement = null;
        }

        private void ClickedPlacement()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, rayMaxDistance, groundLayer))
            {
                Vector2Int hitGridPosition = gridTilesManager.ConvertToGridPosition(hit.point, tileSize);
                TileData hitTileData = gridTilesManager.GetBlockDataAtGridPosition(hitGridPosition);
                if (hitTileData != null)
                {
                    PlacementData hitPlacementData = hitTileData.PlacementData;
                    //if (hitPlacementData != null && hitPlacementData.GrowthTier == WildGrowthTierEnum.MiddleGrowth)
                    {
                        MovePreview();
                        SelectNewPlacement(hitPlacementData);
                        return;
                    }
                }
            }

            // No Placement to select so Null..
            selectedPlacement = null;
        }

        private void SelectNewPlacement(PlacementData placementData)
        {
            selectedPlacement = placementData;
            previewObject3x3Plant.PreviewObjectEnabler(true);
            //Debug.Log("New Selected Placement");
        }

        private void AttemptPlacement()
        {
            if (!previewObject3x3Plant.IsPlaceableHardChecker(selectedPlacement))
                return;

            HashSet<TileData> occupiedGridTileDatas = selectedPlacement.OccupiedGridTiles;
            //Debug.Log($"Occupied on this before: {occupiedGridTileDatas.Count}");
            selectedPlacement.ClearOccupiedGridTiles();
            //Debug.Log($"Occupied on this after: {occupiedGridTileDatas.Count}");

            Vector3 worldPreviewPosition = previewObject3x3Plant.GetCenterPosition();
            Vector2Int previewPosition = gridTilesManager.ConvertToGridPosition(previewObject3x3Plant.transform.position, 1);
            HashSet<Vector2Int> newGridTiles = gridTilesManager.GetGridAreaPositions(previewPosition, 1);

            // Bind to Tiles..
            foreach (var gridTile in newGridTiles)
            {
                TileData tempData = gridTilesManager.GetBlockDataAtGridPosition(gridTile);
                selectedPlacement.AddOccupiedGridTiles(tempData);
            }

            selectedPlacement.PlacementObject.transform.position = worldPreviewPosition;
            //PlantBehaviour plant = selectedPlacement.TryGetPlantBehaviour();
            //plant.GotMoved();

            previewObject3x3Plant.PreviewObjectEnabler(false);
            selectedPlacement = null;
        }

        public void MoveToNewPosition(Vector3 newPosition)
        {
            this.transform.position = newPosition;
        }

        private Vector3 GetSnappedPosition(Vector3 original, Vector3 offset)
        {
            float newX = Mathf.Round(original.x / tileSize) * tileSize + offset.x;
            float newY = 0f + offset.y;
            float newZ = Mathf.Round(original.z / tileSize) * tileSize + offset.z;

            return new Vector3(newX, newY, newZ);
        }
    }
}