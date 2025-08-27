using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MaterialBoard : MonoBehaviour
{
    [SerializeField] private ColorProfile _colorProfile;
    [SerializeField] private MaterialTemplates _materialTemplates;
    [SerializeField] private Color _missingColor;

    private Dictionary<string, Material> _materialMap;
    private List<Material> _gridMaterials;
    private Material _materialMissing;

    public Material MaterialMissing { get => _materialMissing; }
    
    public Dictionary<string, Material> MaterialMap 
    { 
        get => _materialMap; 
    }

    public void InitializeAll()
    {
        CreateMissingMaterial();
        ColorProfileToMaterialMap();
        AddGridMaterials();
        DebugLogAllMaterials();
    }

    private void CreateMissingMaterial()
    {
        _materialMissing = new(_materialTemplates.Unlit)
        { color = _missingColor, name = "Missing Color"};
    }

    public Material GetRandomGridMaterial()
    {
        if (_gridMaterials == null || _gridMaterials.Count == 0)
            return null;

        int index = Random.Range(0, _gridMaterials.Count);
        int i = 0;

        foreach (var mat in _gridMaterials)
        {
            if (i == index)
                return mat;
            i++;
        }

        return null;
    }

    private void ColorProfileToMaterialMap()
    {
        if (_colorProfile == null || _colorProfile.ColorSlots.Count == 0)
            return;

        _materialMap = new();
        List<ColorSlot> colorSlots = _colorProfile.ColorSlots;

        foreach (var slot in colorSlots)
        {
            string colorName = slot.Name;

            Material newMaterial = new(_materialTemplates.Unlit)
            { color = slot.Color, name = colorName };

            _materialMap[colorName] = newMaterial;
        }
    }

    private void AddGridMaterials()
    {
        if (_gridMaterials == null)
            _gridMaterials = new();

        foreach (var item in _materialMap)
        {
            string key = item.Key;
            Material mat = item.Value;

            if (!key.StartsWith("G"))
                return;

            if (!_gridMaterials.Contains(mat))
                _gridMaterials.Add(mat);
        }
    }

    void DebugLogAllMaterials()
    {
        if (_materialMap == null || _materialMap.Count == 0)
            return;

        foreach (var item in _materialMap)
        {
            string name = item.Key;
            Material material = item.Value;
            Debug.Log($"Name: {name}, Color: {material.color}");
        }
    }

    public Material GetMaterial(string name)
    {
        if (_materialMap.TryGetValue(name, out var material))
        {
            return material;
        }

        Debug.LogWarning($"Missing color for {name}");
        return MaterialMissing;
    }

}
