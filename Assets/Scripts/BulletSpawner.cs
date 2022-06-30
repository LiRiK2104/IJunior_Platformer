using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public static BulletSpawner Instance { get; private set; }

    private List<Bullet> _pool = new List<Bullet>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        Bullet.Killing += OnKilling;
    }

    private void OnDisable()
    {
        Bullet.Killing -= OnKilling;
    }
    
    private void OnKilling(Bullet bullet)
    {
        _pool.Add(bullet);
    }
    
    public Bullet Spawn(Weapon weapon)
    {
        var spawnedBullet = _pool.FirstOrDefault(bullet => bullet.GetType() == weapon.Bullet.GetType());

        if (spawnedBullet != null)
            _pool.Remove(spawnedBullet);
        else
            spawnedBullet = Instantiate(weapon.Bullet, weapon.BulletSpawnPoint.position, Quaternion.identity, transform);
        
        spawnedBullet.Init(weapon);

        return spawnedBullet;
    }
}
