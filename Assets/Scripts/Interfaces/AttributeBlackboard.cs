using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttributeBlackboard", menuName = "Scriptable Objects/AttributeBlackboard")]
public class AttributeBlackboard : ScriptableObject
{
    public List<AttributeData> availableAttributes = new List<AttributeData>();
}
