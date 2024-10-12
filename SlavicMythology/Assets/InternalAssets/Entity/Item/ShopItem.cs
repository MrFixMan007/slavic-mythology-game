using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public int price;
    public GameObject itemPrefab;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Coin playerCoin = other.GetComponent<Coin>();

            if (playerCoin != null && playerCoin.GetCurrentCoin() >= price)
            {
                playerCoin.SpendCoin(price);
                SpawnItemBehindPlayer(other.transform);
                Destroy(gameObject);
            }
        }
    }

    private void SpawnItemBehindPlayer(Transform playerTransform)
    {
        Vector3 spawnPosition = playerTransform.position - playerTransform.up;
        Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
    }
}