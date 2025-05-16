using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneSlot : MonoBehaviour, ISlot
{
    public GeneBlackboard geneBlackboard;
    public GeneManager geneManager;
    public GeneData geneData;
    public Image geneIconVisual;

    public event System.Action<GeneSlot> OnSlotChanged;

    public void UpdateSlot()
    {

    }

    public void NextGene()
    {
        ChangeGeneData(true);
    }

    public void PrevGene()
    {
        ChangeGeneData(false);
    }

    public void ChangeGeneData(bool forward)
    {
        int index = geneBlackboard.availableGenes.IndexOf(geneData);

        if (index == -1)
        {
            Debug.LogWarning("Data not found!");
            return;
        }

        int direction = forward ? 1 : -1;
        int newIndex = index + direction;
        int maxIndex = geneBlackboard.availableGenes.Count - 1;

        if (newIndex >= 0 && newIndex <= maxIndex)
            geneData = geneBlackboard.availableGenes[newIndex];
        else if (newIndex > maxIndex)
            geneData = geneBlackboard.availableGenes[0];
        else if (newIndex < 0)
            geneData = geneBlackboard.availableGenes[maxIndex];

        geneIconVisual.sprite = geneData.geneSprite;

        OnSlotChanged?.Invoke(this);
    }
}
