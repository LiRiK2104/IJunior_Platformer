using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves = new List<Wave>();
    [SerializeField] private List<Transform> _spawnPoints = new List<Transform>();
    
    private int _spawnPointIndex = 0;

    private void Start()
    {
        StartCoroutine(PlayWaves());
    }

    private IEnumerator PlayWaves()
    {
        for (int i = 0; i < _waves.Count; i++)
        {
            yield return PlayWave(_waves[i]);
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
        var spawnedEnemy = Instantiate(template, GetSpawnPoint().position, quaternion.identity);
        spawnedEnemy.Init();
        RaiseSpawnPointIndex();
    }

    private Transform GetSpawnPoint()
    {
        return  _spawnPoints[_spawnPointIndex];
    }

    private void RaiseSpawnPointIndex()
    {
        _spawnPointIndex++;
        
        if (_spawnPointIndex >= _spawnPoints.Count)
            _spawnPointIndex = 0;
    }
}
