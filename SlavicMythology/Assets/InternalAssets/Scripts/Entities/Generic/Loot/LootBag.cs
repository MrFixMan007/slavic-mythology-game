using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    public List<Loot> lootList = new List<Loot>();
    public int rolls = 1;
    public float dropForce = 300f;

    List<Loot> GetItem()
    {
        int randomNumber;
        List<Loot> possibleItems = new List<Loot>();
        for (int i = 0; i < rolls; i++)
        {
            foreach (Loot item in lootList)
            {
                randomNumber = Random.Range(1, 101);
                if (randomNumber <= item.dropChance)
                {
                    possibleItems.Add(item);
                }
            }
        }
        if (possibleItems.Count > 0)
        {
            return possibleItems;
        }
        else 
        {
            Debug.Log("No loot?");
            return null;
        }
    }

    public void InstantiateLoot(Vector3 spawnPos) 
    {
        List<Loot> dropped = GetItem();
        if (dropped.Count > 0) 
        {
            foreach (Loot item in dropped) 
            {
                GameObject lootGOJ = Instantiate(item.lootPrefab, spawnPos, Quaternion.identity);

                Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                lootGOJ.GetComponent<Rigidbody2D>().AddForce(direction * dropForce, ForceMode2D.Impulse);
            }
        }
    }
}
