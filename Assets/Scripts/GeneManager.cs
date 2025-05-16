using System.Collections.Generic;
using UnityEngine;

public class GeneManager : MonoBehaviour, ISubManager
{
    public GeneSelection geneSelection;
    public GeneStatistics geneStatistics;
    public List<GeneData> genePool;
    public List<AttributeData> attributePool;

    public void GeneChanged()
    {
        ApplyEffectors();
    }

    void SetManager(GeneSlot slot)
    {
        geneSelection.ChangeGeneManager(slot, this);
    }

    void SetGenes(GeneSlot slot, GeneData data)
    {
        geneSelection.ChangeGeneData(slot, data);
    }

    void ApplyEffectors()
    {
        /*
        foreach (var slot in geneSelection.geneSlots)
        {
            foreach (var stat in geneStatistics.geneStats)
            {
                stat.geneCurrentAmount = 0;

                foreach (var effector in slot.geneData.geneEffectors)
                {
                    if (stat.geneAttribute == effector.geneAttribute)
                        stat.geneCurrentAmount += effector.geneEffectAmount;
                }
            }
        }
        */
    }

    void Start()
    {
        //foreach (var slot in geneStatistics.geneStats)
        {
            //SetManager(slot);
            //SetGenes(slot, genePool[0]);
        }

        foreach (var slot in geneSelection.geneSlots)
        {
            SetManager(slot);
            SetGenes(slot, genePool[0]);
        }

        ApplyEffectors();
    }
}
