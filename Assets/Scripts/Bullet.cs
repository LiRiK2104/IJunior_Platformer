using System;
using System.Collections;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour, IDamagable, ILifetimeOwner
{
    private Rigidbody2D _rigidbody;
    private int _damage;
    
    public static event Action<Bullet> Killing;
    
    public float AllLifetime { get; private set; }
    public float Lifetime { get; private set; }

    public int GetDamage()
    {
        return _damage;
    }
    
    public void Init(Weapon weapon)
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _damage = weapon.Damage;
        AllLifetime = weapon.BulletLifetime;
        Lifetime = 0;
        
        gameObject.transform.position = weapon.BulletSpawnPoint.position;
        gameObject.SetActive(true);
        StartCoroutine(CountLifetime());
    }

    public void Push(Vector3 direction)
    {
        int force = 20;
        _rigidbody.AddForce(direction * force, ForceMode2D.Impulse);
    }

    private void Die()
    {
        // TODO: добавить эффект (необязательно)
        Killing?.Invoke(this);
        gameObject.SetActive(false);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponents<MonoBehaviour>()
                .FirstOrDefault(component => component is IHealthOwner) is IHealthOwner healthOwner)
        {
            healthOwner.TakeDamage(this);
            Die();
        }
    }

    private IEnumerator CountLifetime()
    {
        while (Lifetime < AllLifetime)
        {
            Lifetime += Time.deltaTime;
            yield return null;
        }

        Die();
    }
}

public interface ILifetimeOwner
{
    public float AllLifetime { get; }
    public float Lifetime { get; }
}