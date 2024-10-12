using UnityEngine;
using VContainer;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Reward
{
    public GameObject RewardPrefab;
    public float SpawnChance;
}

public class RoomTrigger2D : MonoBehaviour
{
    private IEnemySpawner _enemySpawner;

    public List<EnemyWave> EnemyWaves = new List<EnemyWave>();
    public List<Reward> PotentialRewards = new List<Reward>(); // Новый список наград
    [SerializeField] public List<GameObject> doors = new List<GameObject>();

    private int _currentWaveIndex = 0;
    private int _remainingEnemies;
    public Transform[] spawnPoints;

    public Transform rewardSpawnPoint;

    private bool hasSpawned = false;

    public void SetEnemySpawner(IEnemySpawner enemySpawner)
    {
        _enemySpawner = enemySpawner;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasSpawned && other.CompareTag("Player"))
        {
            foreach (var door in doors)
            {
                door.GetComponent<Door>().close();
            }
            hasSpawned = true;
            StartCoroutine(SpawnWaves(rewardSpawnPoint.position));
        }
    }

    private IEnumerator SpawnWaves(Vector3 rewardSpawnPoint)
    {
        while (_currentWaveIndex < EnemyWaves.Count)
        {
            var currentWave = EnemyWaves[_currentWaveIndex];
            _remainingEnemies = currentWave.EnemyCount;
            _enemySpawner.SpawnWave(GetSpawnPoint(), new List<EnemyWave> { currentWave }, this);

            while (_remainingEnemies > 0)
            {
                yield return null;
            }

            _currentWaveIndex++;
        }

        SpawnReward(rewardSpawnPoint);

        foreach (var door in doors)
        {
            door.GetComponent<Door>().open();
        }
    }

    private void SpawnReward(Vector3 position)
    {
        foreach (var reward in PotentialRewards)
        {
            if (Random.value <= reward.SpawnChance)
            {
                Instantiate(reward.RewardPrefab, position, Quaternion.identity);
                break; // Предполагаем один приз
            }
        }
    }

    private Vector3 GetSpawnPoint()
    {
        if (spawnPoints.Length > 0)
        {
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            return spawnPoints[spawnIndex].position;
        }

        Debug.LogWarning("Не указаны точки спавна!");
        return transform.position;
    }

    // Вызывается, когда враг уничтожен
    public void EnemyDefeated()
    {
        _remainingEnemies--;
        if (_remainingEnemies < 0)
        {
            _remainingEnemies = 0; // Защита от отрицательных значений
        }
    }
}
