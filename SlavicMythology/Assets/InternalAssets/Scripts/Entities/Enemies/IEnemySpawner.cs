using UnityEngine;
using VContainer;
using VContainer.Unity;
public interface IEnemySpawner
{
    void SpawnEnemies(Vector3 position);
}

public class EnemySpawner : IEnemySpawner
{
    private readonly GameObject _enemyPrefab;
    private readonly float _spawnRadius;

    public EnemySpawner(GameObject enemyPrefab, float spawnRadius)
    {
        _enemyPrefab = enemyPrefab;
        _spawnRadius = spawnRadius;
    }

    private Vector3 GetRandomPositionAroundPlayer(Vector3 playerPosition, float radius)
    {
        // Генерируем случайный угол и радиус
        float angle = Random.Range(0, 2 * Mathf.PI);
        float distance = Random.Range(0, radius);

        // Вычисляем координаты точки
        float x = playerPosition.x + distance * Mathf.Cos(angle);
        float y = playerPosition.y + distance * Mathf.Sin(angle);

        return new Vector3(x, y, playerPosition.z);
    }

    public void SpawnEnemies(Vector3 position)
    {
        // Генерируем случайную позицию вокруг игрока в заданном радиусе
        Vector3 spawnPosition = GetRandomPositionAroundPlayer(position, _spawnRadius);

        // Спауним врага в вычисленной позиции
        GameObject.Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
    }
}