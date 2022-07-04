using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave", menuName = "Wave/Create Wave", order = 51)]
public class Wave : ScriptableObject
{
    [SerializeField] private int _enemiesCount;
    [SerializeField] private float _spawnInterval;
    [SerializeField] private List<ProbabilityCell<Enemy>> _possibleEnemies = new List<ProbabilityCell<Enemy>>();

    public int EnemiesCount => _enemiesCount;
    public float SpawnInterval => _spawnInterval;
    
    private Roulette _roulette;

    public Enemy GetRandomEnemy()
    {
        if (_roulette == null)
            InitializeRoulette();
        
        int index = _roulette.Roll();
        
        if (index >= 0 && index < _possibleEnemies.Count)
        {
            return _possibleEnemies[index].Item;
        }

        return null;
    }
    
    private void InitializeRoulette()
    {
        var probabilities = new Dictionary<int, double>();
        
        for (int i = 0; i < _possibleEnemies.Count; i++)
        {
            probabilities.Add(i, _possibleEnemies[i].Probability);
        }

        _roulette = new Roulette(probabilities);
    }

    private void OnValidate()
    {
        int maxEnemiesCount = 100;
        _enemiesCount = Math.Min(_enemiesCount, maxEnemiesCount);
        
        int minSpawnInterval = 1;
        int maxSpawnInterval = 30;
        _spawnInterval = Mathf.Clamp(_spawnInterval, minSpawnInterval, maxSpawnInterval);

        ProbabilitiesValidator.Validate(_possibleEnemies);
    }
}

static class ProbabilitiesValidator
{
    private const int MinProbability = 0;
    private const int MaxProbability = 100;

    public static void Validate<T>(List<ProbabilityCell<T>> items)
    {
        float probabilitiesSum = 0;

        foreach (var item in items)
        {
            item.Probability = Mathf.Clamp(item.Probability, MinProbability, MaxProbability);

            if (probabilitiesSum + item.Probability > MaxProbability)
            {
                item.Probability = MaxProbability - probabilitiesSum;
            }

            probabilitiesSum += item.Probability;
        }
    }
}

[Serializable]
public class ProbabilityCell<T>
{
    [SerializeField] private T _item;
    [SerializeField] private float _probability;
    
    public T Item => _item;

    public float Probability
    {
        get
        {
            return _probability;
        }

        set
        {
            _probability = value;
        }
    }
}
