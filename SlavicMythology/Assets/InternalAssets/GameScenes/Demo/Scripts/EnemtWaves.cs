using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyType
{
    public GameObject EnemyPrefab;
    public int Count;
}

[System.Serializable]
public class EnemyWave
{
    public List<EnemyType> EnemyTypes = new List<EnemyType>();

    public int EnemyCount
    {
        get
        {
            int total = 0;
            foreach (var enemyType in EnemyTypes)
            {
                total += enemyType.Count;
            }
            return total;
        }
    }
}