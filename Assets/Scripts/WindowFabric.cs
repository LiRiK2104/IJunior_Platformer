using System;
using System.Collections.Generic;
using UnityEngine;

public class WindowFabric : MonoBehaviour
{
    [SerializeField] private List<Window> _windowsTemplates = new List<Window>();

    private static WindowFabric _instance;

    public static WindowFabric Instance => _instance;

    public void Open(int windowNumber)
    {
        if (Enum.TryParse(windowNumber.ToString(), out WindowType windowType) && 
            TryGetWindowTemplate(windowType, out Window template))
        {
            Window window = GetWindow(template);
            window.Open();
        }
    }
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private Window Create(Window template)
    {
        return Instantiate(template, transform.position, Quaternion.identity, transform);
    }

    private Window GetWindow(Window template)
    {
        Window window = null;
        
        var windowObject = GetComponentInChildren(template.GetType(), true);
            
        if (windowObject == null || (windowObject is Window) == false)
            window = Create(template);
        else
            window = windowObject as Window;

        return window;
    }

    private bool TryGetWindowTemplate(WindowType windowType, out Window foundWindow)
    {
        foundWindow = null;
        
        foreach (var window in _windowsTemplates)
        {
            if (window.WindowType == windowType)
            {
                foundWindow = window;
                return true;
            }
        }

        return false;
    }
}

public enum WindowType
{
    Store = 0,
    FailWindow = 1
}
