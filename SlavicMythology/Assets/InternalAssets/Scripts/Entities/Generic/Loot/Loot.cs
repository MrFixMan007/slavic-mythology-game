using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Loot : ScriptableObject
{
    [SerializeField] public GameObject lootPrefab;
    public int dropChance;

    public Loot(int dropChance)
    {
            this.dropChance = dropChance;
    }
}
