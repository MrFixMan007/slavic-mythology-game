using UnityEngine;

public class WheatSpawner : MonoBehaviour
{
    public GameObject[] wheatPrefabs; // Массив префабов колосков пшеницы
    public int numberOfWheat = 10; // Количество колосков, которое необходимо создать
    public Vector2 spawnArea = new Vector2(5, 5); // Область спавна

    void Start()
    {
        SpawnWheat();
    }

    void SpawnWheat()
    {
        for (int i = 0; i < numberOfWheat; i++)
        {
            // Генерация случайной позиции
            Vector2 spawnPosition = new Vector2(
                Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
                Random.Range(-spawnArea.y / 2, spawnArea.y / 2)
            );

            // Выбор случайного префаба из массива
            GameObject wheatPrefab = wheatPrefabs[Random.Range(0, wheatPrefabs.Length)];

            // Создание экземпляра колоска
            Instantiate(wheatPrefab, spawnPosition, Quaternion.identity);
        }
    }
}