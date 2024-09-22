using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Для теста, что монеты тратятся
public class Merchant : MonoBehaviour
{
    public int itemCost = 50;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
          
            Coin playerCoin = other.GetComponent<Coin>();

            if (playerCoin != null)
            {
                if (playerCoin.GetCurrentCoin() >= itemCost)
                {
                    playerCoin.SpendCoin(itemCost);
                    Debug.Log("Предмет куплен!");
                    
                }
                else
                {
                    Debug.Log("Не хватает монет для покупки.");
                }
            }
            else
            {
                Debug.Log("Компонент CoinUI не найден на игроке.");
            }
        }
    }

}
