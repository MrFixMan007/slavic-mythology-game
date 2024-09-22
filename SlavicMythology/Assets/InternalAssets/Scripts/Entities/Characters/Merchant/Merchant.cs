using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��� �����, ��� ������ ��������
[RequireComponent(typeof(LootBag))]
public class Merchant : MonoBehaviour
{
    public int itemCost = 50;
    private LootBag lootBag;

    private void OnValidate()
    {
        lootBag ??= GetComponent<LootBag>();
    }

    private void Awake()
    {
        lootBag ??= GetComponent<LootBag>();
    }

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
                    lootBag.InstantiateLoot(transform.position);
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
