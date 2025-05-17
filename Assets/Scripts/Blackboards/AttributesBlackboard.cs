using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttributesBlackboard", menuName = "Scriptable Objects/AttributesBlackboard")]
public class AttributesBlackboard : ScriptableObject
{
    public List<AttributeData> availableAttributes = new List<AttributeData>();
}
