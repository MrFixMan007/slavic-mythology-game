using System;
using System.Collections;
using FSM.Animation;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rollSpeed = 10f;
    public float rollDuration = 0.5f;
    private Rigidbody2D _rb;
    private Health _health;
    private bool isRolling = false;
    private Vector2 rollDirection;

    public float attackRadius = 1f;
    public int attackDamage = 10;

    [SerializeField] private Animator _animator;
    private AnimFsm _animFsm;

    [Inject]
    public void Construct(Health health)
    {
        _health = health;
        _animator ??= GetComponent<Animator>();
        _animFsm = AnimFsm.CreatePlayerSampleAnimFsm(animator: _animator);
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator ??= GetComponent<Animator>();
    }

    private void Start()
    {
        _animFsm ??= AnimFsm.CreatePlayerSampleAnimFsm(animator: _animator);
    }

    private void Update()
    {
        if (!isRolling)
        {
            HandleMovement();
        }

        HandleAttack();
        HandleRoll();
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal > 0)
        {
            _animFsm.SetState(AnimEnums.WalkRight);
        }
        else if (horizontal < 0)
        {
            _animFsm.SetState(AnimEnums.WalkLeft);
        }
        else if (vertical > 0)
        {
            _animFsm.SetState(AnimEnums.WalkBack);
        }
        else if (vertical < 0)
        {
            _animFsm.SetState(AnimEnums.WalkFront);
        }

        if (horizontal == 0 && vertical == 0)
        {
            _animFsm.SetState(AnimEnums.IdleFront);
        }

        Vector2 moveDirection = new Vector2(horizontal, vertical).normalized;
        _rb.velocity = moveDirection * moveSpeed;
    }

    private void HandleAttack()
    {
        if (Input.GetMouseButtonDown(0)) // ��� ��� �����
        {
            _animFsm.SetState(AnimEnums.AttackFront);
            DetermineAttackDirection();
            Attack();
        }
    }

    private void DetermineAttackDirection()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 playerPosition = transform.position;

        float deltaX = mousePosition.x - playerPosition.x;
        float deltaY = mousePosition.y - playerPosition.y;

        if (Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
        {
            if (deltaX > 0)
            {
                Debug.Log("����� ������");
            }
            else
            {
                Debug.Log("����� �����");
            }
        }
        else
        {
            if (deltaY > 0)
            {
                Debug.Log("����� ������");
            }
            else
            {
                Debug.Log("����� �����");
            }
        }
    }

    private void HandleRoll()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isRolling)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            if (horizontal != 0 || vertical != 0)
            {
                rollDirection = new Vector2(horizontal, vertical).normalized;
                StartCoroutine(Roll());
            }
        }
    }

    private IEnumerator Roll()
    {
        isRolling = true;

        // ������������ �������� ��������
        // �� ����� GetComponent<Animator>().SetTrigger("Roll");

        float elapsedTime = 0f;
        while (elapsedTime < rollDuration)
        {
            _rb.velocity = rollDirection * rollSpeed;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isRolling = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isRolling)
        {
            StopAllCoroutines();
            isRolling = false;
            _rb.velocity = Vector2.zero;
        }
    }

    private void Attack()
    {
        //UnityEngine.Debug.Log("Player attacked!");
        //Vector3 mousePos = Input.mousePosition;
        //UnityEngine.Debug.Log("Player attacked! on x=" + mousePos.x + " on y = " + mousePos.y);
        // ������ ��� �����
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldMousePos.z = 0;

        Vector2 attackDirection = (worldMousePos - transform.position).normalized;
        Vector2 attackPosition = (Vector2)transform.position + attackDirection * attackRadius;

        RaycastHit2D hit = Physics2D.CircleCast(transform.position, attackRadius, attackDirection, attackRadius);

        if (hit.collider != null && hit.collider.CompareTag("Enemy"))
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(attackDamage);
            }
        }
    }

    public void GetDamage(int damage)
    {
        _health.TakeDamage(damage);
    }
}