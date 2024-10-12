using System.Collections.Generic;
using Core.Battle;
using FSM.Animation;
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
            float hp, float attackRadius, SeekerMovement seekerMovement, Animator animator) : base(fsm: fsm,
            target: target, path: path,
            rb: rb,
            detectionRadius: detectionRadius, hp: hp, animator: animator)
        {
            SeekerMovement = seekerMovement; //
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
            float hp, SeekerMovement seekerMovement, ISimpleBattleService simpleBattleService, float attackRadius,
            Animator animator) : base(fsm: fsm, target: target,
            path: path, rb: rb,
            detectionRadius: detectionRadius, hp: hp, attackRadius: attackRadius, seekerMovement: seekerMovement,
            animator: animator)
        {
            _simpleBattleService = simpleBattleService;
        }

        public override void Update()
        {
            base.Update();

            Vector2 vector = Rb.velocity;

            if (vector.x > 3f)
            {
                Debug.Log("x: " + vector.x);
                Animator.ResetTrigger(AnimStates.IdleLeft.ToString());
                Animator.ResetTrigger(AnimStates.IdleBack.ToString());
                Animator.ResetTrigger(AnimStates.IdleFront.ToString());
                
                Animator.ResetTrigger(AnimStates.AttackLeft.ToString());
                Animator.ResetTrigger(AnimStates.AttackBack.ToString());
                Animator.ResetTrigger(AnimStates.AttackFront.ToString());
                Animator.ResetTrigger(AnimStates.AttackRight.ToString());

                Animator.SetTrigger(AnimStates.IdleRight.ToString());
            }
            else if (vector.x < -3)
            {
                Debug.Log("x: " + vector.x);
                Animator.ResetTrigger(AnimStates.IdleRight.ToString());
                Animator.ResetTrigger(AnimStates.IdleBack.ToString());
                Animator.ResetTrigger(AnimStates.IdleFront.ToString());
                
                Animator.ResetTrigger(AnimStates.AttackLeft.ToString());
                Animator.ResetTrigger(AnimStates.AttackBack.ToString());
                Animator.ResetTrigger(AnimStates.AttackFront.ToString());
                Animator.ResetTrigger(AnimStates.AttackRight.ToString());

                Animator.SetTrigger(AnimStates.IdleLeft.ToString());
            }
            else if (vector.y > 5f)
            {
                Debug.Log("y: " + vector.y);
                Animator.ResetTrigger(AnimStates.IdleLeft.ToString());
                Animator.ResetTrigger(AnimStates.IdleRight.ToString());
                Animator.ResetTrigger(AnimStates.IdleFront.ToString());
                
                Animator.ResetTrigger(AnimStates.AttackLeft.ToString());
                Animator.ResetTrigger(AnimStates.AttackBack.ToString());
                Animator.ResetTrigger(AnimStates.AttackFront.ToString());
                Animator.ResetTrigger(AnimStates.AttackRight.ToString());

                Animator.SetTrigger(AnimStates.IdleBack.ToString());
            }
            else if (vector.y < -5)
            {
                Debug.Log("y: " + vector.y);
                Animator.ResetTrigger(AnimStates.IdleLeft.ToString());
                Animator.ResetTrigger(AnimStates.IdleRight.ToString());
                Animator.ResetTrigger(AnimStates.IdleBack.ToString());
                
                Animator.ResetTrigger(AnimStates.AttackLeft.ToString());
                Animator.ResetTrigger(AnimStates.AttackBack.ToString());
                Animator.ResetTrigger(AnimStates.AttackFront.ToString());
                Animator.ResetTrigger(AnimStates.AttackRight.ToString());

                Animator.SetTrigger(AnimStates.IdleFront.ToString());
            }

            if (!_simpleBattleService.CanHit)
            {
                _simpleBattleService.Update();
            }
            else
            {
                if (((Vector2)Target.position - Rb.position).magnitude <= AttackRadius &&
                    _simpleBattleService.CanHit)
                {
                    if (vector.x > 0.5f)
                    {
                        Debug.Log("x: " + vector.x);
                        Animator.ResetTrigger(AnimStates.IdleLeft.ToString());
                        Animator.ResetTrigger(AnimStates.IdleBack.ToString());
                        Animator.ResetTrigger(AnimStates.IdleFront.ToString());
                        Animator.ResetTrigger(AnimStates.IdleRight.ToString());

                        Animator.ResetTrigger(AnimStates.AttackLeft.ToString());
                        Animator.ResetTrigger(AnimStates.AttackBack.ToString());
                        Animator.ResetTrigger(AnimStates.AttackFront.ToString());

                        Animator.SetTrigger(AnimStates.AttackRight.ToString());
                    }
                    else if (vector.x < -0.5f)
                    {
                        Debug.Log("x: " + vector.x);
                        Animator.ResetTrigger(AnimStates.IdleRight.ToString());
                        Animator.ResetTrigger(AnimStates.IdleBack.ToString());
                        Animator.ResetTrigger(AnimStates.IdleFront.ToString());
                        Animator.ResetTrigger(AnimStates.IdleLeft.ToString());

                        Animator.ResetTrigger(AnimStates.AttackRight.ToString());
                        Animator.ResetTrigger(AnimStates.AttackBack.ToString());
                        Animator.ResetTrigger(AnimStates.AttackFront.ToString());

                        Animator.SetTrigger(AnimStates.AttackLeft.ToString());
                    }
                    else if (vector.y > 0.5f)
                    {
                        Debug.Log("y: " + vector.y);
                        Animator.ResetTrigger(AnimStates.IdleLeft.ToString());
                        Animator.ResetTrigger(AnimStates.IdleRight.ToString());
                        Animator.ResetTrigger(AnimStates.IdleFront.ToString());
                        Animator.ResetTrigger(AnimStates.IdleBack.ToString());

                        Animator.ResetTrigger(AnimStates.AttackRight.ToString());
                        Animator.ResetTrigger(AnimStates.AttackLeft.ToString());
                        Animator.ResetTrigger(AnimStates.AttackFront.ToString());

                        Animator.SetTrigger(AnimStates.AttackBack.ToString());
                    }
                    else if (vector.y < -0.5f)
                    {
                        Debug.Log("y: " + vector.y);
                        Animator.ResetTrigger(AnimStates.IdleLeft.ToString());
                        Animator.ResetTrigger(AnimStates.IdleRight.ToString());
                        Animator.ResetTrigger(AnimStates.IdleBack.ToString());
                        Animator.ResetTrigger(AnimStates.IdleFront.ToString());

                        Animator.ResetTrigger(AnimStates.AttackRight.ToString());
                        Animator.ResetTrigger(AnimStates.AttackLeft.ToString());
                        Animator.ResetTrigger(AnimStates.AttackBack.ToString());

                        Animator.SetTrigger(AnimStates.AttackFront.ToString());
                    }
                    _simpleBattleService.Attack();
                }
                else SeekerMovement.MoveChar();
            }
        }
    }
}