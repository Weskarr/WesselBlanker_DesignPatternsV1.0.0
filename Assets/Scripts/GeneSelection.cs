using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GeneSelection : MonoBehaviour
{
    public List<GeneSlot> geneSlots;

    public void ChangeGeneData(GeneSlot slot, GeneData data)
    {
        int index = geneSlots.IndexOf(slot);

        if (index != -1)
            geneSlots[index].geneData = data;
    }

    public void ChangeGeneManager(GeneSlot slot, GeneManager manager)
    {
        int index = geneSlots.IndexOf(slot);

        if (index != -1)
        geneSlots[index].geneManager = manager;
    }
}