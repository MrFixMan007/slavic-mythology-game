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
[RequireComponent(typeof(LootBag))]
[RequireComponent(typeof(Animator))]
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

    private IRoomProducer _roomProducer;

    private LootBag _lbg;

    private FsmEnemy _fsm;
    private SeekerMovement _seekerMovement;
    public event Action OnDefeated;
    [SerializeField] private Animator _animator;

    private void OnValidate()
    {
        _rb ??= GetComponent<Rigidbody2D>();
        _seeker ??= GetComponent<Seeker>();
        _lbg ??= GetComponent<LootBag>();
        _animator ??= GetComponent<Animator>();
    }

    private void Awake()
    {
        _rb ??= GetComponent<Rigidbody2D>();
        _seeker ??= GetComponent<Seeker>();
        _lbg ??= GetComponent<LootBag>();
        _animator ??= GetComponent<Animator>();
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
            detectionRadius: detectionRadius, hp: hp, animator: _animator));

        _fsm.AddState(new FsmStateMeleeSimpleAgr(fsm: _fsm, target: _target, path: _seekerMovement.Path, rb: _rb,
            detectionRadius: detectionRadius, hp: hp, simpleBattleService: simpleMeleeAttackService,
            attackRadius: attackRadius, seekerMovement: _seekerMovement, animator: _animator));

        _fsm.AddState(new FsmStateSimpleStun(fsm: _fsm, target: _target, path: _seekerMovement.Path, rb: _rb,
            detectionRadius: detectionRadius, hp: hp, simpleBattleService: simpleMeleeAttackService, stanTime: 1f,
            animator: _animator, seekerMovement: _seekerMovement));

        _fsm.AddState(new FsmStateForcedPushDie(fsm: _fsm, target: _target, path: _seekerMovement.Path, rb: _rb,
            detectionRadius: detectionRadius, hp: hp, force: 7f, gameObject: this, destroyDelay: 1f, minSpeed: 1f,
            animator: _animator));

        _fsm.SetState<FsmStateAggressive>();

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

    /*public void SetAggresive()
    {
        _fsm.SetState<FsmStateAggressive>();
    }*/

    public void Destroy()
    {
        _lbg.InstantiateLoot(transform.position);
        OnDefeated?.Invoke();

        //_roomProducer.KillMob();

        Destroy(gameObject);
    }
}