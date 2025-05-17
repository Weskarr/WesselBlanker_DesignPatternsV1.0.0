using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlantPanelManager : MonoBehaviour, IManager
{
    [SerializeField] private GeneticsSubManager geneticsSubManager;
    [SerializeField] private AttributesSubManager attributesSubManager;

    private void Start()
    {
        SubscribeGeneticsSubManager();
        geneticsSubManager.RandomizeSlots();
        geneticsSubManager.SubscribeSlots();
        geneticsSubManager.RecalculateAttributes();
        RebindSubManagers();
    }

    private void SubscribeGeneticsSubManager()
    {
        geneticsSubManager.OnAttributesPassiveChange += HandleAttributesPassiveChanged;
    }

    private void UnsubscribeGeneticsSubManager()
    {
        geneticsSubManager.OnAttributesPassiveChange -= HandleAttributesPassiveChanged;
    }

    private void HandleAttributesPassiveChanged()
    {
        Debug.Log("AttributesPassive changed! Rebinding..");
        RebindSubManagers();
    }

    public void RebindSubManagers()
    {
        attributesSubManager.PowerLevel = geneticsSubManager.PowerLevel;
        attributesSubManager.LifespanLevel = geneticsSubManager.LifespanLevel;
        attributesSubManager.AffinityLevel = geneticsSubManager.AffinityLevel;
        attributesSubManager.NimblenessLevel = geneticsSubManager.NimblenessLevel;
        attributesSubManager.ToughnessLevel = geneticsSubManager.ToughnessLevel;
    }
}
