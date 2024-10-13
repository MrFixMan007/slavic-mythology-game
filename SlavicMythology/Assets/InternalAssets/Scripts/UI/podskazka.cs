using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Podskazka : MonoBehaviour
{
    public string itemName;
    public string itemDescription;

    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text itemDescriptionText;
    [SerializeField] private GameObject hintPanel;
    [SerializeField] private Button closeButton;

    private bool isPlayerInRange = false;
    private GameObject player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            ShowHintPanel();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HideHintPanel();
        }
    }

    private void ShowHintPanel()
    {
        if (hintPanel != null)
        {
            hintPanel.SetActive(true);
        }

        itemNameText.text = itemName;
        itemDescriptionText.text = itemDescription;
        isPlayerInRange = true;

        if (closeButton != null)
        {
            closeButton.onClick.AddListener(DestroyHintPanel);
        }
    }

    private void HideHintPanel()
    {
        if (hintPanel != null)
        {
            hintPanel.SetActive(false);
        }
        isPlayerInRange = false;
    }

    private void DestroyHintPanel()
    {
        Destroy(gameObject);
    }
}