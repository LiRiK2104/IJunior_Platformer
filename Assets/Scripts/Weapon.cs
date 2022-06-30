using System;
using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _shootingInterval;
    [SerializeField] private Bullet bullet;
    [SerializeField] private float _bulletLifetime;
    [SerializeField] private Transform _bulletSpawnPoint;
    
    private bool _isWaitInterval = false;
    
    public int Damage => _damage;
    public Bullet Bullet => bullet;
    public float BulletLifetime => _bulletLifetime;
    public Transform BulletSpawnPoint => _bulletSpawnPoint;

    public void Shoot(Vector3 target)
    {
        if (_isWaitInterval == false)
        {
            var bullet = BulletSpawner.Instance.Spawn(this);
            bullet.Push(GetShootDirection(target));
            StartCoroutine(WaitInterval());
        }
    }

    private Vector3 GetShootDirection(Vector3 target)
    {
        return (target - transform.position).normalized;
    }

    private IEnumerator WaitInterval()
    {
        _isWaitInterval = true;
        yield return new WaitForSeconds(_shootingInterval);
        _isWaitInterval = false;
    }
}
