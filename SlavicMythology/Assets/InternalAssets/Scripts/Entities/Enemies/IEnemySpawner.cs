using UnityEngine;
using System.Collections.Generic;

public interface IEnemySpawner
{
    void SpawnWave(Vector3 roomCenter, List<EnemyWave> enemyWaves, RoomTrigger2D roomTrigger);
}

public class EnemySpawner : IEnemySpawner
{
    private readonly float _spawnRadius;
    private readonly LayerMask _obstacleMask;
    private readonly int _maxAttempts = 10;

    public EnemySpawner(float spawnRadius, LayerMask obstacleMask)
    {
        _spawnRadius = spawnRadius;
        _obstacleMask = obstacleMask;
    }

    private Vector3 GetSpawnPosition(Vector3 roomCenter)
    {
        for (int attempt = 0; attempt < _maxAttempts; attempt++)
        {
            float angle = Random.Range(0, 2 * Mathf.PI);
            float distance = Random.Range(0, _spawnRadius);

            float x = roomCenter.x + distance * Mathf.Cos(angle);
            float z = roomCenter.z + distance * Mathf.Sin(angle);

            Vector3 spawnPosition = new Vector3(x, roomCenter.y, z);

            if (!Physics.CheckSphere(spawnPosition, 1f, _obstacleMask))
            {
                return spawnPosition;
            }
        }

        return roomCenter;
    }

    public void SpawnWave(Vector3 roomCenter, List<EnemyWave> enemyWaves, RoomTrigger2D roomTrigger)
    {
        Debug.Log("Спавним волну врагов.");
        foreach (var wave in enemyWaves)
        {
            foreach (var enemyType in wave.EnemyTypes)
            {
                for (int i = 0; i < enemyType.Count; i++)
                {
                    Vector3 spawnPosition = GetSpawnPosition(roomCenter);
                    GameObject enemy = GameObject.Instantiate(enemyType.EnemyPrefab, spawnPosition, Quaternion.identity);
                    Debug.Log($"Спавним врага {enemy.name} в позиции {spawnPosition}");

                    var enemyComponent = enemy.GetComponent<Enemy>();
                    if (enemyComponent != null)
                    {
                        enemyComponent.OnDefeated += () =>
                        {
                            Debug.Log("Враг уничтожен.");
                            roomTrigger.EnemyDefeated();
                        };
                    }
                }
            }
        }
    }
}