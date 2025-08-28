using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ButtonRelay<TEnum>
{
    [SerializeField] public Button button;
    [SerializeField] public TEnum givenType;
}
