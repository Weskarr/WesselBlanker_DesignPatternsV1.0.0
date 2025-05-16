using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gene", menuName = "Scriptable Objects/Gene")]
public class GeneData : ScriptableObject
{
    public Sprite geneSprite;
    public List<GeneEffector> geneEffectors;
}

[System.Serializable]
public class GeneEffector
{
    public PlantAcronymEnum geneAttribute;
    public int geneEffectAmount;
}