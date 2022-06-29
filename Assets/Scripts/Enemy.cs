using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IHealthOwner
{
    [SerializeField] private int _maxHealth;

    private int _health;
    
    public static event Action<Enemy> Killing;
    public event Action<Enemy> TookDamage;
    
    public int MaxHealth => _maxHealth;
    public int Health => _health;

    public void Init()
    {
        gameObject.SetActive(true);
        _health = _maxHealth;
    }

    public void TakeDamage(IDamagable damagableObject)
    {
        _health -= damagableObject.GetDamage();
        _health = Math.Max(_health, 0);

        TookDamage?.Invoke(this);
        
        if (_health == 0)
            Die();
    }

    private void Die()
    {
        Killing?.Invoke(this);
        gameObject.SetActive(false);
    }

    private void OnValidate()
    {
        _maxHealth = Math.Max(_maxHealth, 0);
    }
}
