using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves = new List<Wave>();
    [SerializeField] private List<Transform> _spawnPoints = new List<Transform>();

    private List<Enemy> _pool = new List<Enemy>();
    private int _killedWaveEnemies;
    private int _waveIndex;

    public static event Action<int, int> EnemyCountChanged;
    public static event Action<int> WaveCountChanged; 

    private void OnEnable()
    {
        Enemy.Killing += OnKilling;
    }

    private void OnDisable()
    {
        Enemy.Killing -= OnKilling;
    }

    private void Start()
    {
        StartCoroutine(PlayWaves());
    }

    private void OnKilling(Enemy enemy)
    {
        _pool.Add(enemy);
        _killedWaveEnemies++;
        EnemyCountChanged?.Invoke(_killedWaveEnemies, _waves[_waveIndex].EnemiesCount);
    }

    private IEnumerator PlayWaves()
    {
        for (int i = 0; i < _waves.Count; i++)
        {
            _waveIndex = i;
            _killedWaveEnemies = 0;
            EnemyCountChanged?.Invoke(_killedWaveEnemies, _waves[_waveIndex].EnemiesCount);
            WaveCountChanged?.Invoke(_waveIndex);
            yield return PlayWave(_waves[i]);
            yield return new WaitUntil(() => _killedWaveEnemies >= _waves[_waveIndex].EnemiesCount);
        }
    }

    private IEnumerator PlayWave(Wave wave)
    {
        int spawnedEnemies = 0;

        while (spawnedEnemies < wave.EnemiesCount)
        {
            Spawn(wave.GetRandomEnemy());
            spawnedEnemies++;
            yield return new WaitForSeconds(wave.SpawnInterval);
        }
    }
    
    private void Spawn<T>(T template) where T : Enemy
    {
        var spawnedEnemy = _pool.FirstOrDefault(enemy => enemy is T);
        Vector3 spawnPoint = GetRandomSpawnPoint().position;

        if (spawnedEnemy != null)
        {
            _pool.Remove(spawnedEnemy);
            spawnedEnemy.transform.position = spawnPoint;
        }
        else
        {
            spawnedEnemy = Instantiate(template, spawnPoint, quaternion.identity);
        }
        
        spawnedEnemy.Init();
    }

    private Transform GetRandomSpawnPoint()
    {
        int index = Random.Range(0, _spawnPoints.Count);
        return  _spawnPoints[index];
    }
}
