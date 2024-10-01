using System;
using System.Collections;
using Core;
using Core.Battle;
using FSM.States;
using Movement;
using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class Enemy : MonoBehaviour, IDestroyableGameObject
{
    public float detectionRadius = 5f;
    public float attackRadius = 1f;
    public float moveSpeed = 2f;
    public int damage = 10;
    public float attackCooldown = 1f;
    public float hp = 100f;

    private Transform _target;
    private Rigidbody2D _rb;
    private Vector2 movement;

    private Seeker _seeker;

    public event Action OnDefeated;

    public GameObject lootPrefab;
    public float lootDropChance = 0.3f;

    private FsmEnemy _fsm;
    private SeekerMovement _seekerMovement;

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
        _target = GameObject.FindGameObjectWithTag("Player")
            .transform; //TODO: Вместо тяжёлого метода поиска игрока по тегу нужно будет его передавать из фабрики

        _seekerMovement = new SeekerMovement(rb: _rb, target: _target, seeker: _seeker, moveSpeed: moveSpeed);

        _fsm = new FsmEnemy();
        SimpleMeleeAttackService simpleMeleeAttackService =
            new SimpleMeleeAttackService(meleeLightAttackCoolDown: attackCooldown, meleeAttackDamage: damage,
                target: _target);
        _fsm.AddState(new FsmStateIdle(fsm: _fsm, target: _target, path: _seekerMovement.Path, rb: _rb,
            detectionRadius: detectionRadius, hp: hp));
        _fsm.AddState(new FsmStateMeleeSimpleAgr(fsm: _fsm, target: _target, path: _seekerMovement.Path, rb: _rb,
            detectionRadius: detectionRadius, hp: hp, simpleBattleService: simpleMeleeAttackService,
            attackRadius: attackRadius, seekerMovement: _seekerMovement));
        _fsm.AddState(new FsmStateSimpleStun(fsm: _fsm, target: _target, path: _seekerMovement.Path, rb: _rb,
            detectionRadius: detectionRadius, hp: hp, simpleBattleService: simpleMeleeAttackService, stanTime: 1f));
        _fsm.AddState(new FsmStateForcedPushDie(fsm: _fsm, target: _target, path: _seekerMovement.Path, rb: _rb,
            detectionRadius: detectionRadius, hp: hp, force: 50f, gameObject: this, destroyDelay: 10f, minSpeed: 1f));
        _fsm.SetState<FsmStatePeaceful>();

        InvokeRepeating(nameof(SeekerUpdate), 0f, 0.5f);
    }

    void FixedUpdate()
    {
        _fsm.Update();
    }

    private void SeekerUpdate()
    {
        _seekerMovement.Update();
    }

    public void TakeDamage(int amount)
    {
        _fsm.Hit(amount);
    }

    public void Destroy()
    {
        if (UnityEngine.Random.value <= lootDropChance && lootPrefab != null)
        {
            Instantiate(lootPrefab, transform.position, Quaternion.identity);
        }

        OnDefeated?.Invoke(); // Вызов события
        Destroy(gameObject);
    }
}