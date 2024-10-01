using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Loot : ScriptableObject
{
    [SerializeField] public GameObject lootPrefab;
    public int dropChance;
    public int guaranteedCount;

    public Loot(int dropChance, int guaranteedCount)
    {
        this.dropChance = dropChance;
        this.guaranteedCount = guaranteedCount;
    }
}
