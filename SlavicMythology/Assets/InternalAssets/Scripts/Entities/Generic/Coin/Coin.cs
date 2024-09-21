using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int maxCoin = 1000;
    private int currentCoin;

    [SerializeField] private TMP_Text coinText;

    private void Awake()
    {
        currentCoin = 0;
        UpdateCoinUI();
    }

    public void AddCoin(int amount)
    {
        currentCoin += amount;
        if (currentCoin >= maxCoin)
        {
            currentCoin = maxCoin;
        }
        UpdateCoinUI();
    }

    public void SpendCoin(int amount)
    {
        currentCoin -= amount;
        if (currentCoin < 0)
        {
            currentCoin = 0;
        }
        UpdateCoinUI();
    }

    public int GetCurrentCoin()
    {
        return currentCoin;
    }

    private void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + currentCoin.ToString();
        }
    }

    

}
