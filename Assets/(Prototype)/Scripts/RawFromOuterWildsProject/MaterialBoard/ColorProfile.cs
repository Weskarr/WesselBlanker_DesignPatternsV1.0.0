using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorProfile", menuName = "Scriptable Objects/ColorProfile")]
public class ColorProfile : ScriptableObject
{
    [SerializeField] private List<ColorSlot> _colorSlots = new();

    public List<ColorSlot> ColorSlots
    { get => _colorSlots; }
}
