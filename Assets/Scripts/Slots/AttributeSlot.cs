using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttributeSlot : MonoBehaviour, ISlot
{
    public int attributeCurrentLevel;
    public AttributeData attributeData;
    [SerializeField] private TextMeshProUGUI attributeLevelText;
    [SerializeField] private TextMeshProUGUI attributeNameText;
    [SerializeField] private Image attributeImage;

    public void UpdateSlot()
    {
        attributeLevelText.text = attributeCurrentLevel.ToString();
        attributeNameText.text = attributeData.attributeName.ToString() + ":";
        attributeImage.sprite = attributeData.attributeIconVisual;
    }
}
