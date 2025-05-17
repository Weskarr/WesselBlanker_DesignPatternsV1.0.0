using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GenesBlackboard", menuName = "Scriptable Objects/GenesBlackboard")]
public class GenesBlackboard : ScriptableObject
{
    public List<GeneData> availableGenes = new List<GeneData>();
}
