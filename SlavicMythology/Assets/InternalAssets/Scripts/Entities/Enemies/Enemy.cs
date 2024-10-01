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

    private FsmEnemy _fsm;
    private SeekerMovement _seekerMovement;

    private LootBag _lbg;

    private void OnValidate()
    {
        _rb ??= GetComponent<Rigidbody2D>();
        _seeker ??= GetComponent<Seeker>();
        _lbg ??= GetComponent<LootBag>();
    }

    private void Awake()
    {
        _rb ??= GetComponent<Rigidbody2D>();
        _seeker ??= GetComponent<Seeker>();
        _lbg ??= GetComponent<LootBag>();
    }

    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player")
            .transform; //TODO: ������ ������� ������ ������ ������ �� ���� ����� ����� ��� ���������� �� �������

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
        _lbg.InstantiateLoot(transform.position);
        Destroy(gameObject);
    }
}