using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class GeneticsSubManager : MonoBehaviour, ISubManager, IPlantAttributesHolder
{
    public List<GeneSlot> slots = new List<GeneSlot>();
    public Dictionary<PlantAcronymEnum, int> attributesPassive = new();

    public event System.Action OnAttributesPassiveChange;

    public void SubscribeSlots()
    {
        foreach (var slot in slots)
        {
            slot.OnSlotChanged += HandleGeneSlotChanged;
        }
    }

    public void UnsubscribeSlots()
    {
        foreach (var slot in slots)
        {
            slot.OnSlotChanged -= HandleGeneSlotChanged;
        }
    }

    private void HandleGeneSlotChanged(GeneSlot slot)
    {
        Debug.Log("GeneSlot changed! Recalculating attributes.");
        RecalculateAttributes();
        OnAttributesPassiveChange?.Invoke();
    }

    public void RecalculateAttributes()
    {
        ZeroAttributes();
        CalculateAttributes();
    }

    public void ZeroAttributes()
    {
        foreach (PlantAcronymEnum type in Enum.GetValues(typeof(PlantAcronymEnum)))
        {
            if (type == PlantAcronymEnum.None) continue;
            attributesPassive[type] = 0;
        }
    }

    public void CalculateAttributes()
    {
        foreach (var slot in slots)
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
