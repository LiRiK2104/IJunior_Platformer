using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        gameObject.SetActive(true);
    }
}
