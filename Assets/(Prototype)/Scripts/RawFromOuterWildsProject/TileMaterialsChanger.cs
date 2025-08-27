using UnityEngine;
using TileSystem;

public class TilePainter : MonoBehaviour
{
    public MaterialBoard _materialBoard;
    public string _materialName;

    public GameObject tileMapParent;
    public TileMap tileMap;

    private Material newMaterial;

    private void Update()
    {
        if (newMaterial == null)
            newMaterial = _materialBoard.GetMaterial(_materialName);

        if (tileMapParent != null && tileMapParent.transform.childCount > 0)
            tileMap = tileMapParent.transform.GetChild(0).GetComponent<TileMap>();

        if (tileMap == null || newMaterial == null)
            return;

        if (tileMap == null || newMaterial == null)
            return;

        if (Input.GetMouseButton(0)) // Left mouse button held
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 hitPos = hit.point;
                Vector2Int gridPos = WorldToGridPosition(hitPos, tileMap.TileSize);

                if (tileMap.TileDictionary.TryGetValue(gridPos, out TileData tile))
                {
                    if (tile.TileMaterial != newMaterial)
                    {
                        tile.TileMaterial = newMaterial;
                        tileMap.RefreshTileInstance(tile, tileMap.TileSize);
                        //Debug.Log($"Painted tile at {gridPos} with {newMaterial.name}");
                    }
                }
            }
        }
    }

    private Vector2Int WorldToGridPosition(Vector3 worldPos, float tileSize)
    {
        int x = Mathf.FloorToInt(worldPos.x / tileSize);
        int y = Mathf.FloorToInt(worldPos.z / tileSize); // Assuming Z is forward
        return new Vector2Int(x, y);
    }
}
