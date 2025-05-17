using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttributesSubManager : MonoBehaviour, ISubManager, IPlantAttributesHolder
{
    public List<AttributeSlot> slots = new List<AttributeSlot>();

    private int GetAttribute(PlantAcronymEnum type)
    {
        AttributeSlot slot = slots.FirstOrDefault(s => s.AttributeData.attributeName == type);

        if (slot != null)
        {
            return slot.attributeCurrentLevel;
        }
        else
        {
            Debug.Log("Missing Attribute Slot!");
            return 0;
        }
    }

    private void SetAttribute(PlantAcronymEnum type, int value)
    {
        AttributeSlot slot = slots.FirstOrDefault(s => s.AttributeData.attributeName == type);
        if (slot != null)
        {
            slot.attributeCurrentLevel = value;
            slot.UpdateSlot();
        }
        else
        {
            Debug.Log("Missing Attribute Slot!");
        }
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