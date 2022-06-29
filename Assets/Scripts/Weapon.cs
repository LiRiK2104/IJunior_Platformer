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

    private BulletSpawner _bulletSpawner;
    private bool isWaitingInterval = false;
    
    public int Damage => _damage;
    public Bullet Bullet => bullet;
    public float BulletLifetime => _bulletLifetime;
    public Transform BulletSpawnPoint => _bulletSpawnPoint;

    private void Awake()
    {
        _bulletSpawner = FindObjectOfType<BulletSpawner>();
    }

    public void Shoot(Vector3 playerPosition)
    {
        if (isWaitingInterval == false)
        {
            Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - playerPosition).normalized;
            var bullet = _bulletSpawner.Spawn(this);
            bullet.Push(direction);
            StartCoroutine(WaitInterval());
        }
    }

    private IEnumerator WaitInterval()
    {
        isWaitingInterval = true;
        yield return new WaitForSeconds(_shootingInterval);
        isWaitingInterval = false;
    }
}
