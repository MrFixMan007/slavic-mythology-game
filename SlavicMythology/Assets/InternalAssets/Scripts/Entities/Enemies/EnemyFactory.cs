using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class EnemyFactory : IEnemyFactory
{
    private readonly IObjectResolver container;
    private readonly GameObject enemyPrefab;

    public EnemyFactory(IObjectResolver container, GameObject enemyPrefab)
    {
        this.container = container;
        this.enemyPrefab = enemyPrefab;
    }

    public Enemy CreateEnemy(Vector3 position)
    {
        GameObject enemyObject = UnityEngine.Object.Instantiate(enemyPrefab, position, Quaternion.identity);
        container.InjectGameObject(enemyObject);
        return enemyObject.GetComponent<Enemy>();
    }
}