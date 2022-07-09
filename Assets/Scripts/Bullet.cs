using System;
using System.Collections;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour, IDamager, ILifetimeOwner
{
    private Rigidbody2D _rigidbody;
    private int _damage;
    
    public static event Action<Bullet> Killing;
    
    public float MaxLifetime { get; private set; }
    public float Lifetime { get; private set; }

    public int Damage => _damage;

    public void Init(Weapon weapon)
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _damage = weapon.Damage;
        MaxLifetime = weapon.BulletLifetime;
        Lifetime = 0;
        
        gameObject.transform.position = weapon.BulletSpawnPoint.position;
        gameObject.SetActive(true);
        Invoke(nameof(Die), MaxLifetime);
    }

    public void Push(Vector3 direction)
    {
        int force = 20;
        _rigidbody.AddForce(direction * force, ForceMode2D.Impulse);
    }

    protected void Die()
    {
        Killing?.Invoke(this);
        gameObject.SetActive(false);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out IHealthOwner healthOwner))
        {
            healthOwner.TakeDamage(this);
            Die();
        }
    }
}

public interface ILifetimeOwner
{
    public float MaxLifetime { get; }
    public float Lifetime { get; }
}