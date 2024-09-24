using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    public List<Loot> lootList = new List<Loot>();
    public int rolls = 1;
    public float dropForce = 300f;

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
                }
            }
        }
        return possibleItems;
    }

    public void InstantiateLoot(Vector3 spawnPos)
    {
        List<Loot> dropped = GetItem();

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