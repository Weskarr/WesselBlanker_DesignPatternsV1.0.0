using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MaterialBoard : MonoBehaviour
{
    [SerializeField] private ColorProfile _colorProfile;
    [SerializeField] private MaterialTemplates _materialTemplates;
    [SerializeField] private Color _missingColor;

    private Dictionary<string, Material> _materialMap;
    private Material _materialMissing;

    public Material MaterialMissing { get => _materialMissing; }
    
    public Dictionary<string, Material> MaterialMap 
    { 
        get => _materialMap; 
    }

    public void InitializeAll()
    {
        //InitializeColorMapFromProfile();
        CreateMissingMaterial();
        ColorProfileToMaterialMap();
        DebugLogAllMaterials();
    }

    private void CreateMissingMaterial()
    {
        _materialMissing = new(_materialTemplates.Unlit)
        { color = _missingColor };
    }

    public Material GetRandomMaterial()
    {
        if (_materialMap == null || _materialMap.Count == 0)
            return null;

        int index = Random.Range(0, _materialMap.Count);
        int i = 0;

        foreach (var mat in _materialMap.Values)
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
            Material newMaterial = new(_materialTemplates.Unlit)
            { color = slot.Color };

            string colorName = slot.Name;
            _materialMap[colorName] = newMaterial;
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

    /*
void InitializeColorMapFromProfile()
{
    materialMap = new Dictionary<MaterialSerieEnum, Dictionary<MaterialTypeEnum, Material>>();

    foreach (var serie in _materialProfile.Series)
    {
        MaterialSerieEnum serieKey = serie.Serie;

        if (!materialMap.ContainsKey(serieKey))
        {
            materialMap[serieKey] = new Dictionary<MaterialTypeEnum, Material>();
        }

        foreach (var template in serie.Templates)
        {
            MaterialTypeEnum typeKey = template.Type;
            Material materialValue = template.Material;

            if (!materialMap[serieKey].ContainsKey(typeKey))
            {
                materialMap[serieKey][typeKey] = materialValue;
            }
        }
    }
}

    */
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
