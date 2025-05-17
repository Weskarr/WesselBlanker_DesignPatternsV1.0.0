using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlantPanelManager : MonoBehaviour, IManager
{
    public GeneticsSubManager geneticsSubManager;
    public AttributesSubManager attributesSubManager;

    private void Start()
    {
        SubscribeGeneticsSubManager();
        geneticsSubManager.SubscribeSlots();
        geneticsSubManager.RecalculateAttributes();
        RebindSubManagers();
    }

    public void SubscribeGeneticsSubManager()
    {
        geneticsSubManager.OnAttributesPassiveChange += HandleAttributesPassiveChanged;
    }

    public void UnsubscribeGeneticsSubManager()
    {
        geneticsSubManager.OnAttributesPassiveChange -= HandleAttributesPassiveChanged;
    }

    private void HandleAttributesPassiveChanged()
    {
        Debug.Log("AttributesPassive changed! Rebinding..");
        RebindSubManagers();
    }

    private void RebindSubManagers()
    {
        attributesSubManager.PowerLevel = geneticsSubManager.PowerLevel;
        attributesSubManager.LifespanLevel = geneticsSubManager.LifespanLevel;
        attributesSubManager.AffinityLevel = geneticsSubManager.AffinityLevel;
        attributesSubManager.NimblenessLevel = geneticsSubManager.NimblenessLevel;
        attributesSubManager.ToughnessLevel = geneticsSubManager.ToughnessLevel;
    }
}
