using UnityEngine;
using System.Collections.Generic;

public class WheatSpawner : MonoBehaviour
{
    public GameObject[] wheatPrefabs; // Массив префабов колосков пшеницы
    public int numberOfWheat = 10; // Количество колосков, которое необходимо создать
    public Vector2 spawnArea = new Vector2(5, 5); // Область спавна
    public float minDistance = 0.5f; // Минимальное расстояние между колосками

    private List<Vector2> spawnPositions = new List<Vector2>(); // Список позиций для хранения размещенных колосков

    void Start()
    {
        SpawnWheat();
    }

    void SpawnWheat()
    {
        for (int i = 0; i < numberOfWheat; i++)
        {
            Vector2 spawnPosition;
            int attempts = 0; // Счетчик попыток найти позицию

            // Ищем подходящую позицию с ограничением по попыткам
            do
            {
                spawnPosition = new Vector2(
                    Random.Range(-spawnArea.x / 2, spawnArea.x / 2),
                    Random.Range(-spawnArea.y / 2, spawnArea.y / 2)
                );

                attempts++;

                // Проверяем, не слишком близко ли это к уже существующим позициям
            } while (IsPositionTooClose(spawnPosition) && attempts < 100);

            if (attempts < 100)
            {
                spawnPositions.Add(spawnPosition); // Добавляем позицию в список
                Instantiate(GetRandomWheatPrefab(), spawnPosition, Quaternion.identity); // Создаем колосок
            }
            else
            {
                Debug.LogWarning("Не удалось найти уникальную позицию для спавна после 100 попыток.");
            }
        }
    }

    // Метод для проверки расстояния до существующих позиций
    private bool IsPositionTooClose(Vector2 position)
    {
        foreach (Vector2 existingPosition in spawnPositions)
        {
            if (Vector2.Distance(position, existingPosition) < minDistance)
            {
                return true; // Позиция слишком близка
            }
        }
        return false; // Позиция уникальна
    }

    // Метод для получения случайного префаба пшеницы
    private GameObject GetRandomWheatPrefab()
    {
        return wheatPrefabs[Random.Range(0, wheatPrefabs.Length)];
    }
}