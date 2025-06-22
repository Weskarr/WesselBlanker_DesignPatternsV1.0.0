using System.Collections.Generic;
using UnityEngine;

public class PlacementData
{
    private Vector2Int _gridPosition;
    private GameObject _placementObject;
    //private WildGrowthTierEnum _growthTier;
    private HashSet<TileData> _occupiedGridTiles = new();

    //public PlantBehaviour TryGetPlantBehaviour()
    //{
     //   _placementObject.TryGetComponent<PlantBehaviour>(out PlantBehaviour component);
    //    return component;
    //}

    public void AddOccupiedGridTiles(TileData gridTileData)
    {
        _occupiedGridTiles.Add(gridTileData);
        gridTileData.PlacementData = this;
    }

    public void RemoveOccupiedGridTiles(TileData gridTileData)
    {
        if (_occupiedGridTiles.Contains(gridTileData))
        {
            _occupiedGridTiles.Remove(gridTileData);
            gridTileData.PlacementData = null;
        }
    }

    public void ClearOccupiedGridTiles()
    {
        foreach(var gridTileData in _occupiedGridTiles)
            gridTileData.PlacementData = null;

        _occupiedGridTiles.Clear();
    }

    public HashSet<TileData> OccupiedGridTiles
    {
        get => _occupiedGridTiles;
        set => _occupiedGridTiles = value;
    }

    public Vector2Int GridPosition
    {
        get => _gridPosition;
        set => _gridPosition = value;
    }

    public GameObject PlacementObject
    {
        get => _placementObject;
        set => _placementObject = value;
    }

    //public WildGrowthTierEnum GrowthTier
    //{
    //    get => _growthTier;
    //    set => _growthTier = value;
    //}
}
