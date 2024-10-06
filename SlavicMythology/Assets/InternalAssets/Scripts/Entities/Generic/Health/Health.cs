using System.Diagnostics;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    
    [SerializeField] private Image healthBar; // ������ �� ����������� ������� ��������
    [SerializeField] private Color normalHealthColor = Color.green; // ���� ��� ���������� ������ ��������
    [SerializeField] private Color criticalHealthColor = Color.red; // ���� ��� ���������� ������ ������ ��������
    [SerializeField] private float criticalHealthThreshold = 0.3f; // ����� ������������ ������ �������� (30% �� maxHealth)

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

        // ���������� ������� ��������
        healthBar.fillAmount = (float)currentHealth / maxHealth;

        // ��������� ����� ������� �������� ��� ������ ������ ��������
        if ((float)currentHealth / maxHealth <= criticalHealthThreshold)
        {
            healthBar.color = criticalHealthColor; // ������� ���� ��� ���������� ������ ������ ��������
        }
        else
        {
            healthBar.color = normalHealthColor; // ������� ���� ��� ���������� ������ ��������
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

        // ���������� ����� ����� ���������
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
        //Heal(amount); // �������� ���� �� �������� ��������
        currentHealth = maxHealth; // ������� ���������
        healthBar.fillAmount = 1f; 
        healthBar.color = normalHealthColor; 
        UpdateHealthUI();
    }


    private void Die()
    {
        UnityEngine.Debug.Log("Player died");
        // ����� ������ ��� ������
        UnityEngine.SceneManagement.SceneManager.LoadScene(
        UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}