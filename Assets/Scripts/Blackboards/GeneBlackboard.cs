using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GeneBlackboard", menuName = "Scriptable Objects/GeneBlackboard")]
public class GeneBlackboard : ScriptableObject
{
    public List<GeneData> availableGenes = new List<GeneData>();
}
