using UnityEngine;
using VContainer;
using System.Collections;
using System.Collections.Generic;

public class RoomTrigger2D : MonoBehaviour
{
    [Inject] private IEnemySpawner _enemySpawner;

    public List<EnemyWave> EnemyWaves = new List<EnemyWave>();
    private int _currentWaveIndex = 0;
    private int _remainingEnemies;
    public Transform[] spawnPoints; //����� ������

    private bool hasSpawned = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasSpawned && other.CompareTag("Player"))
        {
            Debug.Log("����� ����� � �������, �������� ����� ����.");
            hasSpawned = true;
            StartCoroutine(SpawnWaves(other.transform.position));
        }
        else
        {
            //Debug.Log("������ ����� � �������, �� ��� �� �����.");
        }
    }  


    private IEnumerator SpawnWaves(Vector3 roomCenter)
    {
        while (_currentWaveIndex < EnemyWaves.Count)
        {
            

            var currentWave = EnemyWaves[_currentWaveIndex];
            _remainingEnemies = currentWave.EnemyCount;
            _enemySpawner.SpawnWave(GetSpawnPoint(), new List<EnemyWave> { currentWave }, this);

            while (_remainingEnemies > 0)
            {
                yield return null; // ����, ���� ��� ����� �� ����� ����������
            }

            _currentWaveIndex++;
        }
    }

    private Vector3 GetSpawnPoint()
    {
        // �������� ��������� ����� ������ �� �������
        if (spawnPoints.Length > 0)
        {
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            return spawnPoints[spawnIndex].position;
        }
        else
        {
            Debug.LogWarning("�� ������� ����� ������!");
            return transform.position; // ������� �� ���������
        }
    }

    // ����������, ����� ���� ���������
    public void EnemyDefeated()
    {
        _remainingEnemies--;
        if (_remainingEnemies < 0) _remainingEnemies = 0; // ������ �� ������������� ��������
    }
}