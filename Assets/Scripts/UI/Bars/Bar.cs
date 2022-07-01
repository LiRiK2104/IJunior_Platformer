using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public abstract class Bar : MonoBehaviour
{
    protected const int MaxAlpha = 1;
    protected const int MinAlpha = 0;
    
    [SerializeField] protected Image BarImage;

    protected float ProtectedValue = 1;
    protected CanvasGroup CanvasGroup { get; private set; }

    protected virtual void Awake()
    {
        CanvasGroup = GetComponent<CanvasGroup>();
    }

    protected virtual void Start()
    {
        BarImage.fillAmount = ProtectedValue;
    }

    protected void OnValueChanged(int value, int maxValue)
    {
        ProtectedValue = (float)value / maxValue;
    }

    private void Update()
    {
        BarImage.fillAmount = Mathf.MoveTowards(BarImage.fillAmount, ProtectedValue, Time.deltaTime);
    }
}
