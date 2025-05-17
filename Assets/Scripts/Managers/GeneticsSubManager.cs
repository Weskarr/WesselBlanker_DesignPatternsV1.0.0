using System;
using System.Collections.Generic;
using UnityEngine;

public class GeneticsSubManager : MonoBehaviour, ISubManager, IPlantAttributesUser
{
    [SerializeField] private List<GeneSlot> geneSlots = new List<GeneSlot>();
    private Dictionary<PlantAcronymEnum, int> attributesPassive = new();

    public event System.Action OnAttributesPassiveChange;

    public void SubscribeSlots()
    {
        foreach (var slot in geneSlots)
        {
            slot.OnSlotChanged += HandleGeneSlotChanged;
        }
    }

    public void UnsubscribeSlots()
    {
        foreach (var slot in geneSlots)
        {
            slot.OnSlotChanged -= HandleGeneSlotChanged;
        }
    }

    public void RandomizeSlots()
    {
        foreach (var slot in geneSlots)
        {
            slot.RandomizeGene();
        }
    }

    public void RecalculateAttributes()
    {
        ZeroAttributes();
        CalculateAttributes();
    }

    private void HandleGeneSlotChanged(GeneSlot slot)
    {
        Debug.Log("GeneSlot changed! Recalculating attributes.");
        RecalculateAttributes();
        OnAttributesPassiveChange?.Invoke();
    }

    private void ZeroAttributes()
    {
        foreach (PlantAcronymEnum type in Enum.GetValues(typeof(PlantAcronymEnum)))
        {
            if (type == PlantAcronymEnum.None) continue;
            attributesPassive[type] = 0;
        }
    }

    private void CalculateAttributes()
    {
        foreach (var slot in geneSlots)
        {
            foreach (var effector in slot.geneData.geneEffectors)
            {
                int amount = effector.geneEffectAmount;
                
                if (effector.geneAttribute == PlantAcronymEnum.None)
                {
                    Debug.LogError("We got a CRAZY none here!");
                    continue;
                }

                if (!attributesPassive.ContainsKey(effector.geneAttribute))
                    attributesPassive[effector.geneAttribute] = 0;

                attributesPassive[effector.geneAttribute] += amount;
            }
        }
    }

    private int GetAttribute(PlantAcronymEnum type)
    {
        if (attributesPassive.TryGetValue(type, out int val))
            return val;
        return 0;
    }

    private void SetAttribute(PlantAcronymEnum type, int value)
    {
        attributesPassive[type] = value;
    }

    public int PowerLevel
    {
        get => GetAttribute(PlantAcronymEnum.Power);
        set => SetAttribute(PlantAcronymEnum.Power, value);
    }

    public int LifespanLevel
    {
        get => GetAttribute(PlantAcronymEnum.Lifespan);
        set => SetAttribute(PlantAcronymEnum.Lifespan, value);
    }

    public int AffinityLevel
    {
        get => GetAttribute(PlantAcronymEnum.Affinity);
        set => SetAttribute(PlantAcronymEnum.Affinity, value);
    }

    public int NimblenessLevel
    {
        get => GetAttribute(PlantAcronymEnum.Nimbleness);
        set => SetAttribute(PlantAcronymEnum.Nimbleness, value);
    }

    public int ToughnessLevel
    {
        get => GetAttribute(PlantAcronymEnum.Toughness);
        set => SetAttribute(PlantAcronymEnum.Toughness, value);
    }
}
