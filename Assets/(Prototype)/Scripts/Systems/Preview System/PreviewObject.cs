using UnityEngine;

namespace PreviewSystem
{
    public class PreviewObject : MonoBehaviour
    {
        [SerializeField] private bool isEnabled;
        [SerializeField] private PreviewTiles previewTiles;

        [SerializeField] private Material canPlaceMat;
        [SerializeField] private Material cannotPlaceMat;
        [SerializeField] private Renderer placementRenderer;

        public void PreviewObjectEnabler(bool direction)
        {
            if (isEnabled == direction)
                return;

            isEnabled = direction;
            placementRenderer.enabled = direction;
            previewTiles.PreviewTilesEnabler(direction);
        }

        public void StartPreview()
        {
            previewTiles.AddChildrenAsPreviewTiles();
        }

        public void UpdatePreview(PlacementData currentPlacement)
        {
            previewTiles.UpdateEachPreviewTile(currentPlacement);
            UpdatePlacementRenderer();
        }

        public Vector3 GetCenterPosition()
        {
            return transform.position;
        }

        public void UpdatePlacementRenderer()
        {
            bool isPlaceable = previewTiles.IsPlaceableReturner();
            if (isPlaceable)
                placementRenderer.sharedMaterial = canPlaceMat;
            else
                placementRenderer.sharedMaterial = cannotPlaceMat;
        }

        public bool IsPlaceableHardChecker(PlacementData currentPlacement)
        {
            previewTiles.UpdateEachPreviewTile(currentPlacement);
            return previewTiles.IsPlaceableReturner();
        }
    }
}