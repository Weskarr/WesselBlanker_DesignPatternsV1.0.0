using UnityEngine;

public class PreviewTileData
{
    [SerializeField] private bool _isPlaceable;
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private Renderer _renderer;

    public bool IsPlaceable
    {
        get => _isPlaceable;
        set => _isPlaceable = value;
    }
    public GameObject GameObject
    {
        get => _gameObject;
        set => _gameObject = value;
    }
    public Renderer Renderer
    {
        get => _renderer;
        set => _renderer = value;
    }
}
