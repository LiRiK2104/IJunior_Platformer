using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public void Init()
    {
        gameObject.SetActive(true);
    }
}
