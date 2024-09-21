using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int coinAmount = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Coin playerCoin = other.GetComponent<Coin>();
            if (playerCoin != null && playerCoin.GetCurrentCoin() < playerCoin.maxCoin) 
            {
                playerCoin.AddCoin(coinAmount);
                Destroy(gameObject);
            }
        }
    }
}
