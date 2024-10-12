using Core.Battle;
using FSM.Animation;
using Movement;
using Pathfinding;
using UnityEngine;

namespace FSM.States
{
    public abstract class FsmStateStun : FsmStateEnemy
    {
        protected float StanTime;
        protected float StanTick;
        protected SeekerMovement SeekerMovement;
        private float _velocityFlag = 1f;

        protected FsmStateStun(FsmEnemy fsm, Transform target, Path path, Rigidbody2D rb, float detectionRadius,
            float hp, float stanTime, Animator animator, SeekerMovement seekerMovement) : base(fsm: fsm, target: target, path: path, rb: rb,
            detectionRadius: detectionRadius,
            hp: hp, animator: animator)
        {
            StanTime = stanTime;
            SeekerMovement = seekerMovement;
        }

        public override void Enter()
        {
            Vector2 vector = Rb.velocity;
            
            // –ассчитываем обратную силу по ос€м X и Y, противоположную текущей скорости
            Vector2 force = -vector * 2f;

            // ѕримен€ем силу к Rigidbody2D
            Rb.AddForce(force, ForceMode2D.Impulse);
            
            if (vector.x > _velocityFlag)
            {
                Animator.SetTrigger(AnimEnums.IdleRight.ToString());
            }
            else if (vector.x < -_velocityFlag)
            {
                Animator.SetTrigger(AnimEnums.IdleLeft.ToString());
            }
            else if (vector.y > _velocityFlag * 2)
            {
                Animator.SetTrigger(AnimEnums.IdleBack.ToString());
            }
            else if (vector.y < _velocityFlag * 2)
            {
                Animator.SetTrigger(AnimEnums.IdleFront.ToString());
            }
            
            StanTick = 0;
        }

        public override void Exit()
        {
            Animator.ResetTrigger(AnimEnums.IdleRight.ToString());
            Animator.ResetTrigger(AnimEnums.IdleLeft.ToString());
            Animator.ResetTrigger(AnimEnums.IdleBack.ToString());
            Animator.ResetTrigger(AnimEnums.IdleFront.ToString());
            StanTick = 0;
        }

        public override void Update()
        {
            base.Update();
            // SeekerMovement.StopChar();
            StanTick += Time.deltaTime;
            if (StanTick > StanTime)
            {
                if (targetIsReachable)
                {
                    Fsm.SetState<FsmStateAggressive>();
                }
                else Fsm.SetState<FsmStatePeaceful>();
            }
        }

        public override void Hit(float hp)
        {
            HealthService.Hit(hp);
            StanTick = 0;
            if (HealthService.CurrentHp <= 0) Fsm.SetState<FsmStateDie>();
        }
    }

    public class FsmStateSimpleStun : FsmStateStun
    {
        protected ISimpleBattleService SimpleBattleService;

        public FsmStateSimpleStun(FsmEnemy fsm, Transform target, Path path, Rigidbody2D rb, float detectionRadius,
            float hp, float stanTime, ISimpleBattleService simpleBattleService, Animator animator, SeekerMovement seekerMovement) : base(fsm: fsm,
            target: target, path: path,
            rb: rb, detectionRadius: detectionRadius,
            hp: hp, stanTime: stanTime, animator: animator, seekerMovement: seekerMovement)
        {
            SimpleBattleService = simpleBattleService;
        }

        public override void Update()
        {
            base.Update();
            SimpleBattleService.Update();
        }
    }
}