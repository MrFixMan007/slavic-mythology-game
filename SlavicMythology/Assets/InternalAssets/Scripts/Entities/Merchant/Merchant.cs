using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��� �����, ��� ������ ��������
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
                    Debug.Log("������� ������!");
                    
                }
                else
                {
                    Debug.Log("�� ������� ����� ��� �������.");
                }
            }
            else
            {
                Debug.Log("��������� CoinUI �� ������ �� ������.");
            }
        }
    }

}
