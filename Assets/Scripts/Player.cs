using System;
using UnityEngine;

public class Player : MonoBehaviour, IHealthOwner
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private Weapon _weapon;
    
    private int _health;
    
    public static event Action Killing;
    public static event Action<Player> TookDamage;
    
    public int MaxHealth => _maxHealth;
    public int Health => _health;

    private void Awake()
    {
        Init();
    }

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
        Killing?.Invoke();
        gameObject.SetActive(false);
    }

    private void OnValidate()
    {
        _maxHealth = Math.Max(_maxHealth, 0);
    }
}

public interface IDamagable
{
    public int GetDamage();
}

public interface IHealthOwner
{
    public int Health { get; }

    public void TakeDamage(IDamagable damagableObject);
}
