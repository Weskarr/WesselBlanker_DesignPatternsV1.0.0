using UnityEngine;

[CreateAssetMenu(fileName = "AttributeData", menuName = "Scriptable Objects/AttributeData")]
public class AttributeData : ScriptableObject
{
    public PlantAcronymEnum attributeName;
    public Sprite attributeIconVisual;
}
