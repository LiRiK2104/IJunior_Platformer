using System;
using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private int _damage;
    [SerializeField] private float _shootingInterval;
    [SerializeField] private Bullet bullet;
    [SerializeField] private float _bulletLifetime;
    [SerializeField] private Transform _bulletSpawnPoint;
    
    private bool _isWaitInterval = false;
    
    public int Id => _id;
    public int Damage => _damage;
    public Bullet Bullet => bullet;
    public float BulletLifetime => _bulletLifetime;
    public Transform BulletSpawnPoint => _bulletSpawnPoint;

    public void Shoot(Vector2 target)
    {
        if (_isWaitInterval == false)
        {
            var bullet = BulletSpawner.Instance.Spawn(this);
            bullet.Push(GetShootDirection(target));
            StartCoroutine(WaitInterval());
        }
    }

    private Vector2 GetShootDirection(Vector2 target)
    {
        return (target - (Vector2) transform.position).normalized;
    }

    private IEnumerator WaitInterval()
    {
        _isWaitInterval = true;
        yield return new WaitForSeconds(_shootingInterval);
        _isWaitInterval = false;
    }
}
