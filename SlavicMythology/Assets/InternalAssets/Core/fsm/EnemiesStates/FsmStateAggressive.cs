using System.Collections.Generic;
using Core.Battle;
using FSM.Animation;
using Movement;
using Pathfinding;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FSM.States
{
    public abstract class FsmStateAggressive : FsmStateEnemy
    {
        protected SeekerMovement SeekerMovement;
        public float AttackRadius;
        protected AnimFsm AnimFsm;

        protected FsmStateAggressive(FsmEnemy fsm, Transform target, Path path, Rigidbody2D rb, float detectionRadius,
            float hp, float attackRadius, SeekerMovement seekerMovement, Animator animator) : base(fsm: fsm,
            target: target, path: path,
            rb: rb,
            detectionRadius: detectionRadius, hp: hp, animator: animator)
        {
            SeekerMovement = seekerMovement;
            AttackRadius = attackRadius;
            AnimFsm = AnimFsm.CreateSampleAnimFsm(animator: animator);
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
        private enum MoveDirectionEnum
        {
            Left,
            Right,
            Forward,
            Back,
        }

        private ISimpleBattleService _simpleBattleService;
        private float _velocityFlag = 1f;

        private MoveDirectionEnum _currentDirection;

        public FsmStateMeleeSimpleAgr(FsmEnemy fsm, Transform target, Path path, Rigidbody2D rb, float detectionRadius,
            float hp, SeekerMovement seekerMovement, ISimpleBattleService simpleBattleService, float attackRadius,
            Animator animator) : base(fsm: fsm, target: target,
            path: path, rb: rb,
            detectionRadius: detectionRadius, hp: hp, attackRadius: attackRadius, seekerMovement: seekerMovement,
            animator: animator)
        {
            _currentDirection = MoveDirectionEnum.Forward;
            _simpleBattleService = simpleBattleService;
        }//
        
        public override void Update()
        {
            base.Update();

            Vector2 vector = Rb.linearVelocity;
            Debug.Log("y " + vector.y);
            Debug.Log("x " + vector.x);

            if (vector.x > _velocityFlag)
            {
                AnimFsm.SetState(AnimEnums.WalkRight);
                _currentDirection = MoveDirectionEnum.Right;
            }
            else if (vector.x < -_velocityFlag)
            {
                AnimFsm.SetState(AnimEnums.WalkLeft);
                _currentDirection = MoveDirectionEnum.Left;
            }
            else if (vector.y > _velocityFlag * 2)
            {
                AnimFsm.SetState(AnimEnums.WalkBack);
                _currentDirection = MoveDirectionEnum.Forward;
            }
            else if (vector.y < _velocityFlag * 2)
            {
                AnimFsm.SetState(AnimEnums.WalkFront);
                _currentDirection = MoveDirectionEnum.Back;
            }
            else
            {
                switch (_currentDirection)
                {
                    case MoveDirectionEnum.Right:
                    {
                        AnimFsm.SetState(AnimEnums.IdleRight);
                        break;
                    }
                    case MoveDirectionEnum.Left:
                    {
                        AnimFsm.SetState(AnimEnums.IdleLeft);
                        break;
                    }
                    case MoveDirectionEnum.Forward:
                    {
                        AnimFsm.SetState(AnimEnums.IdleFront);
                        break;
                    }
                    case MoveDirectionEnum.Back:
                    {
                        AnimFsm.SetState(AnimEnums.IdleBack);
                        break;
                    }
                }
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
                    switch (_currentDirection)
                    {
                        case MoveDirectionEnum.Right:
                        {
                            AnimFsm.SetState(AnimEnums.AttackRight);
                            break;
                        }
                        case MoveDirectionEnum.Left:
                        {
                            AnimFsm.SetState(AnimEnums.AttackLeft);
                            break;
                        }
                        case MoveDirectionEnum.Forward:
                        {
                            AnimFsm.SetState(AnimEnums.AttackFront);
                            break;
                        }
                        case MoveDirectionEnum.Back:
                        {
                            AnimFsm.SetState(AnimEnums.AttackBack);
                            break;
                        }
                    }

                    _simpleBattleService.Attack();
                }
                else SeekerMovement.MoveChar();
            }
        }
    }
}