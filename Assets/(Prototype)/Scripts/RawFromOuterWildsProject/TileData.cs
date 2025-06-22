using System;
using System.Linq;
using UnityEngine;

public class TileData
{
    // Visuals:
    private GameObject _object;
    private Renderer _renderer;
    private Material _tileMaterial;

    // Position
    private Vector2Int _gridPosition;

    // Ownership
    private bool _isClaimable = true;
    private FactionEnum _FactionOwner;
    private float _tileHealthCur;

    // Occupied
    private PlacementData _placementData;

    // Events
    //public event Action<GridTileData, Material> OnChangedMaterial;
    public event Action<TileData, FactionEnum> OnChangedFactionOwner;

    // Neighbors
    public TileData[] Neighbors
    { get; private set; } = new TileData[4];

    public void AddNeighbor(int directionIndex, TileData neighbor)
    {
        if (neighbor != null && !Neighbors.Contains(neighbor))
        {
            Neighbors[directionIndex] = neighbor;
        }
    }

    public void RemoveNeighbor(int directionIndex)
    {
        if (Neighbors[directionIndex] != null)
        {
            Neighbors[directionIndex] = null;
        }
    }

    public Material TileMaterial
    {
        get => _tileMaterial;
        set => _tileMaterial = value;
    }

    public GameObject TileGameObject
    {
        get => _object;
        set => _object = value;
    }

    public Renderer TileRenderer
    {
        get => _renderer;
        set => _renderer = value;
    }

    public bool IsClaimable
    {
        get => _isClaimable;
        set => _isClaimable = value;
    }

    public FactionEnum FactionOwner
    {
        get => _FactionOwner;
        set
        {
            if (_FactionOwner == value) return;
            _FactionOwner = value;

            OnChangedFactionOwner?.Invoke(this, _FactionOwner);
        }
    }

    public Vector2Int GridPosition
    {
        get => _gridPosition;
        set => _gridPosition = value;
    }

    public float TileHealthCur
    {
        get => _tileHealthCur;
        set => _tileHealthCur = value;
    }

    public PlacementData PlacementData
    {
        get => _placementData;
        set => _placementData = value;
    }
}