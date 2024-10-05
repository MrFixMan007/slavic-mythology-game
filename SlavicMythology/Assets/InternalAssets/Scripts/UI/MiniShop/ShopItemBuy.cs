using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemBuy : MonoBehaviour
{
    public string itemName;
    public string itemDescription;
    public int itemCost;
    public ItemEffect itemEffect;
    public AudioClip purchaseSound;
    public Color blinkColor = Color.yellow;

    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text itemDescriptionText;
    [SerializeField] private TMP_Text itemPriceText;
    [SerializeField] private GameObject purchasePanel;
    [SerializeField] private Button purchaseButton;
    [SerializeField] private AudioSource audioSource;

    private bool isPlayerInRange = false;
    private GameObject player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            ShowPurchasePanel();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HidePurchasePanel();
        }
    }

    private void ShowPurchasePanel()
    {
        if (purchasePanel != null)
        {
            purchasePanel.SetActive(true);
        }

        itemNameText.text = itemName;
        itemDescriptionText.text = itemDescription;
        itemPriceText.text = "Златников: " + itemCost.ToString();
        isPlayerInRange = true;

        if (purchaseButton != null)
        {
            purchaseButton.onClick.RemoveAllListeners();
            purchaseButton.onClick.AddListener(PurchaseItem);
        }
    }

    private void HidePurchasePanel()
    {
        if (purchasePanel != null)
        {
            purchasePanel.SetActive(false);
        }
        isPlayerInRange = false;
    }

    public void PurchaseItem()
    {
        if (isPlayerInRange && player != null)
        {
            Coin playerCoin = player.GetComponent<Coin>();

            if (playerCoin != null && playerCoin.GetCurrentCoin() >= itemCost)
            {
                playerCoin.SpendCoin(itemCost);
                StartCoroutine(PlayPurchaseSound());
            }
        }
    }

    private IEnumerator PlayPurchaseSound()
    {
        audioSource.PlayOneShot(purchaseSound);
        StartCoroutine(BlinkPlayer());
        yield return new WaitForSeconds(purchaseSound.length);
        itemEffect.Apply(player);

        Destroy(gameObject);
        HidePurchasePanel();
    }

    private IEnumerator BlinkPlayer()
    {
        Color originalColor = player.GetComponent<SpriteRenderer>().color;

        while (true)
        {
            player.GetComponent<SpriteRenderer>().color = blinkColor;
            yield return new WaitForSeconds(0.1f);
            player.GetComponent<SpriteRenderer>().color = originalColor;
            yield return new WaitForSeconds(0.1f);

            // Если игрок получил урон, то выходим из корутины
            if (player.GetComponent<Health>().GetCurrentHealth() < player.GetComponent<Health>().maxHealth)
            {
                player.GetComponent<SpriteRenderer>().color = originalColor;
                yield break;
            }
        }
    }
}