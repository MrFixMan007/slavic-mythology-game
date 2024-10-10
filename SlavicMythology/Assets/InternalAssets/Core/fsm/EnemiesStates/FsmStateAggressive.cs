using Core.Battle;
using Movement;
using Pathfinding;
using UnityEngine;

namespace FSM.States
{
    public abstract class FsmStateAggressive : FsmStateEnemy
    {
        protected SeekerMovement SeekerMovement;
        public float AttackRadius;

        protected FsmStateAggressive(FsmEnemy fsm, Transform target, Path path, Rigidbody2D rb, float detectionRadius,
            float hp, float attackRadius, SeekerMovement seekerMovement) : base(fsm: fsm, target: target, path: path,
            rb: rb,
            detectionRadius: detectionRadius, hp: hp)
        {
            SeekerMovement = seekerMovement;//
            AttackRadius = attackRadius;
        }

        public override void Enter()
        {
        }

        public override void Exit()
        {
        }

        // public override void Update()
        // {
        //     base.Update();
        //     if (!targetIsReachable)
        //     {
        //         Fsm.SetState<FsmStatePeaceful>();
        //     }
        // }
    }

    public class FsmStateMeleeSimpleAgr : FsmStateAggressive
    {
        private ISimpleBattleService _simpleBattleService;

        public FsmStateMeleeSimpleAgr(FsmEnemy fsm, Transform target, Path path, Rigidbody2D rb, float detectionRadius,
            float hp, SeekerMovement seekerMovement, ISimpleBattleService simpleBattleService, float attackRadius) : base(fsm: fsm, target: target,
            path: path, rb: rb,
            detectionRadius: detectionRadius, hp: hp, attackRadius: attackRadius, seekerMovement: seekerMovement)
        {
            _simpleBattleService = simpleBattleService;
        }

        public override void Update()
        {
            base.Update();

            if (!_simpleBattleService.CanHit)
            {
                _simpleBattleService.Update();
            }
            else
            {
                if (((Vector2)Target.position - Rb.position).magnitude <= AttackRadius &&
                    _simpleBattleService.CanHit)
                {
                    _simpleBattleService.Attack();
                }
                else SeekerMovement.MoveChar();
            }
        }
    }
}