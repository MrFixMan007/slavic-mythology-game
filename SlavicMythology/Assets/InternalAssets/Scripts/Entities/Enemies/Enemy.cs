using System;
using System.Collections;
using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class Enemy : MonoBehaviour
{
    public float detectionRadius = 5f;
    public float attackRadius = 1f;
    public float moveSpeed = 2f;
    public int damage = 10;
    public float attackCooldown = 1f;
    public float hp = 100f;

    private Transform _target;
    private bool _canAttack = true;
    private Rigidbody2D _rb;
    private Vector2 movement;

    [SerializeField] private float _nextWaypointDistance = 3f;
    private Path _path;
    private int _currentWayPoint = 0;

    private Seeker _seeker;

    private void OnValidate()
    {
        _rb ??= GetComponent<Rigidbody2D>();
        _seeker ??= GetComponent<Seeker>();
    }

    private void Awake()
    {
        _rb ??= GetComponent<Rigidbody2D>();
        _seeker ??= GetComponent<Seeker>();
    }

    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;

        InvokeRepeating(nameof(UpdatePath), 0f, 0.5f);
    }

    private void UpdatePath()
    {
        if (_seeker.IsDone())
            _seeker.StartPath(_rb.position, _target.position, OnPathComplete);
    }


    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            _path = p;
            _currentWayPoint = 0;
        }
    }

    void FixedUpdate()
    {
        if (_path != null)
        {
            Vector3 direction = _target.position - transform.position;
            float distanceToPlayer = direction.magnitude;

            if (distanceToPlayer <= detectionRadius)
            {
                // Преследуем игрока
                MoveChar();

                if (distanceToPlayer <= attackRadius && _canAttack)
                {
                    StartCoroutine(Attack());
                }
            }
        }
    }

    private void MoveChar()
    {
        Vector2 direction = ((Vector2)_path.vectorPath[_currentWayPoint] - _rb.position).normalized;
        Vector2 movePosition = (Vector2)transform.position + (direction * moveSpeed * Time.deltaTime);
        _rb.MovePosition(movePosition);

        float distance = Vector2.Distance(_rb.position, _path.vectorPath[_currentWayPoint]);
        if (distance < _nextWaypointDistance && _currentWayPoint < _path.vectorPath.Count - 1)
        {
            _currentWayPoint++;
        }
    }

    private IEnumerator Attack()
    {
        _canAttack = false;

        // Логика нанесения урона
        if (_target.TryGetComponent(out Health playerHealth))
        {
            playerHealth.TakeDamage(damage);
        }

        yield return new WaitForSeconds(attackCooldown);
        _canAttack = true;
    }

    private void Die()
    {
        GetComponent<LootBag>().InstantiateLoot(transform.position);
        Destroy(gameObject);
    }
}