using System.Diagnostics;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D _rb;
    private Health _health;

    [Inject]
    public void Construct(Health health)
    {
        _health = health;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleMovement();
        HandleAttack();
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 moveDirection = new Vector2(horizontal, vertical).normalized;
        _rb.velocity = moveDirection * moveSpeed;
    }

    private void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0)) // ЛКМ для атаки
        {
            Attack();
        }
    }

    private void Attack()
    {
        UnityEngine.Debug.Log("Player attacked!");
        Vector3 mousePos = Input.mousePosition;
        {
            UnityEngine.Debug.Log("Player attacked! on x=" + mousePos.x + " on y = " + mousePos.y);
        }
        // Логика для атаки
    }

    public void GetDamage(int damage)
    {
        _health.TakeDamage(damage);
    }
}