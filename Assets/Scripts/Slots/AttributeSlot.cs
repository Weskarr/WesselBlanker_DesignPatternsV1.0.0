using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttributeSlot : MonoBehaviour, ISlot
{
    public int attributeCurrentLevel;
    public AttributeData AttributeData;
    public TextMeshProUGUI attributeLevelText;
    public TextMeshProUGUI attributeNameText;
    public Image attributeImage;

    public void UpdateSlot()
    {
        attributeLevelText.text = attributeCurrentLevel.ToString();
        attributeNameText.text = AttributeData.attributeName.ToString() + ":";
        attributeImage.sprite = AttributeData.attributeIconVisual;
    }
}
