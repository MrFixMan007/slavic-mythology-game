using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    public List<Loot> lootList = new List<Loot>();
    public int rolls = 1;
    public float dropForce = 50f;

    private List<Loot> GetItems()
    {
        List<Loot> possibleItems = new List<Loot>();

        foreach (Loot item in lootList)
        {
            if (item.guaranteedCount > 0)
            {
                for (int i = 0; i < item.guaranteedCount; i++) 
                {
                    possibleItems.Add(item);
                }
            }
        }

        for (int i = 0; i < rolls; i++)
        {
            foreach (Loot item in lootList)
            {
                int randomNumber = Random.Range(1, 101);
                if (randomNumber <= item.dropChance)
                {
                    possibleItems.Add(item);
                }
            }
        }
        return possibleItems;
    }

    private List<Loot> GetItem()
    {
        List<Loot> possibleItems = new List<Loot>();

        for (int i = 0; i < rolls; i++)
        {
            foreach (Loot item in lootList)
            {
                int randomNumber = Random.Range(1, 101);
                if (randomNumber <= item.dropChance)
                {
                    possibleItems.Add(item);
                    return possibleItems;
                }
            }
        }
        return possibleItems;
    }

    public void InstantiateLoot(Vector3 spawnPos, int dropMethod = 0)
    {
        List<Loot> dropped = null;
        switch (dropMethod)
        {
            case 2:
                break;
            case 1:
                dropped = GetItem();
                break;
            case 0:
                dropped = GetItems();
                break;
            default:
                dropped = GetItems();
                break;
        }

        if (dropped == null || dropped.Count == 0)
        {
            Debug.Log("No loot dropped.");
            return;
        }

        foreach (Loot item in dropped)
        {
            GameObject lootGOJ = Instantiate(item.lootPrefab, spawnPos, Quaternion.identity);
            Rigidbody2D rb = lootGOJ.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
                rb.AddForce(direction * dropForce, ForceMode2D.Impulse);
            }
        }
    }
}