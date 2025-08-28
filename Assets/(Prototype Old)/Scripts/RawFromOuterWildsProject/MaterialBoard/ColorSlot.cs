using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ColorSlot
{
    [SerializeField] private string _name;
    [SerializeField] private Color _color;

    public string Name { get => _name; }
    public Color Color{ get => _color; }
}