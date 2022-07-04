using UnityEngine;
using UnityEngine.UI;

public abstract class Window : MonoBehaviour
{
    [SerializeField] protected Button _closeButton;

    public abstract WindowType WindowType { get; }
    
    public virtual void Open()
    {
        gameObject.SetActive(true);
    }
    
    public virtual void Close()
    {
        gameObject.SetActive(false);
    }
    
    private void OnEnable()
    {
        _closeButton.onClick.AddListener(Close);
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveListener(Close);
    }
}
