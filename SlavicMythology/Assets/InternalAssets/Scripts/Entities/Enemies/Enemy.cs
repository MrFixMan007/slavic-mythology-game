using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    public float detectionRadius = 5f;
    public float attackRadius = 1f;
    public float moveSpeed = 2f;
    public int damage = 10;
    public float attackCooldown = 1f;

    private Transform _player;
    private bool _canAttack = true;
    private Rigidbody2D _rigidBody2D;
    private Vector2 movement;

    private void OnValidate()
    {
        _rigidBody2D ??= GetComponent<Rigidbody2D>();
    }

    private void Awake()
    {
        _rigidBody2D ??= GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (_player != null)
        {
            Vector3 direction = _player.position - transform.position;
            float distanceToPlayer = direction.magnitude;
            movement = direction.normalized;
            
            if (distanceToPlayer <= detectionRadius)
            {
                // Преследуем игрока
                MoveChar(movement);

                if (distanceToPlayer <= attackRadius && _canAttack)
                {
                    StartCoroutine(Attack());
                }
            }
        }
    }
    
    private void MoveChar(Vector2 direction)
    {
        _rigidBody2D.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    private IEnumerator Attack()
    {
        _canAttack = false;

        // Логика нанесения урона
        if (_player.TryGetComponent(out Health playerHealth))
        {
            playerHealth.TakeDamage(damage);
        }

        yield return new WaitForSeconds(attackCooldown);
        _canAttack = true;
    }
}