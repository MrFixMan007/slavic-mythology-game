using UnityEngine;

public class WheatSpawner : MonoBehaviour
{
    public GameObject[] wheatPrefabs; // ������ �������� �������� �������
    public int numberOfWheat = 10; // ���������� ��������, ������� ���������� �������
    public Vector2 spawnArea = new Vector2(5, 5); // ������� ������

    void Start()
    {
        SpawnWheat();
    }

    void SpawnWheat()
    {
        for (int i = 0; i < numberOfWheat; i++)
        {
            // ��������� ��������� �������
            Vector2 spawnPosition = new Vector2(
                Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
                Random.Range(-spawnArea.y / 2, spawnArea.y / 2)
            );

            // ����� ���������� ������� �� �������
            GameObject wheatPrefab = wheatPrefabs[Random.Range(0, wheatPrefabs.Length)];

            // �������� ���������� �������
            Instantiate(wheatPrefab, spawnPosition, Quaternion.identity);
        }
    }
}