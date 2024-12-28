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
    private Vector2 movementDirection; // Направление ходьбы

    [SerializeField]  private float attackCooldown = 1f; //  кулдаун атаки
    private float lastAttackTime = 0f;

    public Collider2D attackArea; // ссылка на коллайдер зоны атаки

    public float attackRadius = 1f;
    public int attackDamage = 10;

    [SerializeField] private Animator _animator;
    private AnimFsm _animFsm;

    public Vector2 attackOffset = Vector2.zero; // Смещение центра атаки


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

        movementDirection = new Vector2(horizontal, vertical);

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

        Vector2 moveDirection = movementDirection.normalized;
        _rb.velocity = moveDirection * moveSpeed;
        UpdateAttackAreaPosition();
    }

    private void UpdateAttackAreaPosition()
    {
        if (movementDirection != Vector2.zero)
        {
            Vector2 adjustedPosition = movementDirection.normalized * attackRadius + attackOffset;
            attackArea.transform.localPosition = adjustedPosition;

            // Проверка направления движения для отражения коллайдера
            if (movementDirection.y < 0)
            {
                attackArea.transform.localScale = new Vector3(-1, 1, 1); // Отражение по горизонтали
            }
            else
            {
                attackArea.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    private void HandleAttack()
    {
        if (Time.time - lastAttackTime >= attackCooldown && Input.GetMouseButtonDown(0)) // ЛКМ для атаки
        {
            _animFsm.SetState(AnimEnums.AttackFront);
            DetermineAttackDirection();
            Attack();
            lastAttackTime = Time.time;
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
                Debug.Log("Атака справа");
            }
            else
            {
                Debug.Log("Атака слева");
            }
        }
        else
        {
            if (deltaY > 0)
            {
                Debug.Log("Атака сверху");
            }
            else
            {
                Debug.Log("Атака снизу");
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

        // Запуск анимации переката

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
        
    public void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(attackArea.bounds.center, attackArea.bounds.size, 0f);

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                Enemy enemy = hit.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(attackDamage);

                    // Отталкивание врага
                    Vector2 pushDirection = (enemy.transform.position - transform.position).normalized;
                    float pushForce = 0.1f; //  сила отталкивания
                    enemy.GetComponent<Rigidbody2D>().AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
                }
            }
        }
    }

    public void GetDamage(int damage)
    {
        _health.TakeDamage(damage);
    }
}