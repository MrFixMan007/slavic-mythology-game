using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float detectionRadius = 5f;
    public float attackRadius = 1f;
    public float moveSpeed = 2f;
    public int damage = 10;
    public float attackCooldown = 1f;

    private Transform _player;
    private bool _canAttack = true;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (_player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, _player.position);

            if (distanceToPlayer <= detectionRadius)
            {
                // Преследуем игрока
                transform.position = Vector2.MoveTowards(transform.position, _player.position, moveSpeed * Time.deltaTime);

                if (distanceToPlayer <= attackRadius && _canAttack)
                {
                    StartCoroutine(Attack());
                }
            }
        }
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
