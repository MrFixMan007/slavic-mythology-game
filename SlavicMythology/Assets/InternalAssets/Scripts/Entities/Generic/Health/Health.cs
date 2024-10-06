using System.Diagnostics;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    
    [SerializeField] private Image healthBar; // Ссылка на изображение полоски здоровья
    [SerializeField] private Color normalHealthColor = Color.green; // Цвет при нормальном уровне здоровья
    [SerializeField] private Color criticalHealthColor = Color.red; // Цвет при критически низком уровне здоровья
    [SerializeField] private float criticalHealthThreshold = 0.3f; // Порог критического уровня здоровья (30% от maxHealth)

    [SerializeField] private TMP_Text healthText;


    private void Awake()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth < 0)
        {
            currentHealth = 0; 
        }

        // Обновление полоски здоровья
        healthBar.fillAmount = (float)currentHealth / maxHealth;

        // Изменение цвета полоски здоровья при низком уровне здоровья
        if ((float)currentHealth / maxHealth <= criticalHealthThreshold)
        {
            healthBar.color = criticalHealthColor; // Красный цвет при критически низком уровне здоровья
        }
        else
        {
            healthBar.color = normalHealthColor; // Зеленый цвет при нормальном уровне здоровья
        }

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = $"{currentHealth}/{maxHealth}";
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        // Обновление цвета после исцеления
        healthBar.fillAmount = (float)currentHealth / maxHealth;

        if ((float)currentHealth / maxHealth <= criticalHealthThreshold)
        {
            healthBar.color = criticalHealthColor;
        }
        else
        {
            healthBar.color = normalHealthColor;
        }

        UpdateHealthUI();
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public void IncreaseMaxHealth(int amount)
    {
        maxHealth += amount;
        //Heal(amount); // увеличит лишь на заданное значение
        currentHealth = maxHealth; // излечит полностью
        healthBar.fillAmount = 1f; 
        healthBar.color = normalHealthColor; 
        UpdateHealthUI();
    }


    private void Die()
    {
        UnityEngine.Debug.Log("Player died");
        // нужна логика для смерти
        UnityEngine.SceneManagement.SceneManager.LoadScene(
        UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}