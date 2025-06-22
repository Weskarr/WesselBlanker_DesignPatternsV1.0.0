using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SliderRelay<TEnum>
{
    [SerializeField] public Slider slider;
    [SerializeField] public TEnum givenType;
}
