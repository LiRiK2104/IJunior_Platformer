using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public abstract class Bar : MonoBehaviour
{
    protected Slider Slider;

    protected virtual void Awake()
    {
        Slider = GetComponent<Slider>();
    }

    protected void OnValueChanged(int value, int maxValue)
    {
        Slider.value = (float)value / maxValue;
    }
}
