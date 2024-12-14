using UnityEngine;
using System.Collections.Generic;

public class WheatSpawner : MonoBehaviour
{
    public GameObject[] wheatPrefabs; // ������ �������� �������� �������
    public int numberOfWheat = 10; // ���������� ��������, ������� ���������� �������
    public Vector2 spawnArea = new Vector2(5, 5); // ������� ������
    public float minDistance = 0.5f; // ����������� ���������� ����� ���������

    private List<Vector2> spawnPositions = new List<Vector2>(); // ������ ������� ��� �������� ����������� ��������

    void Start()
    {
        SpawnWheat();
    }

    void SpawnWheat()
    {
        for (int i = 0; i < numberOfWheat; i++)
        {
            Vector2 spawnPosition;
            int attempts = 0; // ������� ������� ����� �������

            // ���� ���������� ������� � ������������ �� ��������
            do
            {
                spawnPosition = new Vector2(
                    Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
                    Random.Range(-spawnArea.y / 2, spawnArea.y / 2)
                );

                attempts++;

                // ���������, �� ������� ������ �� ��� � ��� ������������ ��������
            } while (IsPositionTooClose(spawnPosition) && attempts < 100);

            if (attempts < 100)
            {
                spawnPositions.Add(spawnPosition); // ��������� ������� � ������
                Instantiate(GetRandomWheatPrefab(), spawnPosition, Quaternion.identity); // ������� �������
            }
            else
            {
                Debug.LogWarning("�� ������� ����� ���������� ������� ��� ������ ����� 100 �������.");
            }
        }
    }

    // ����� ��� �������� ���������� �� ������������ �������
    private bool IsPositionTooClose(Vector2 position)
    {
        foreach (Vector2 existingPosition in spawnPositions)
        {
            if (Vector2.Distance(position, existingPosition) < minDistance)
            {
                return true; // ������� ������� ������
            }
        }
        return false; // ������� ���������
    }

    // ����� ��� ��������� ���������� ������� �������
    private GameObject GetRandomWheatPrefab()
    {
        return wheatPrefabs[Random.Range(0, wheatPrefabs.Length)];
    }
}