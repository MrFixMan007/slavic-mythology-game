using Core.HealthService;
using FSM.States;
using Pathfinding;
using UnityEngine;

namespace FSM
{
    public abstract class FsmStateEnemy
    {
        protected readonly FsmEnemy Fsm;
        protected Transform Target;
        protected Path Path;
        protected Rigidbody2D Rb;
        protected float DetectionRadius;
        protected IHealthService HealthService;
        protected bool targetIsReachable;
        protected Animator Animator;

        protected FsmStateEnemy(FsmEnemy fsm, Transform target, Path path, Rigidbody2D rb, float detectionRadius,
            float hp, Animator animator)
        {
            Fsm = fsm;
            Target = target;
            Path = path;
            Rb = rb;
            DetectionRadius = detectionRadius;
            HealthService = new HealthService(hp);
            Animator = animator;
        }

        protected bool TargetIsReachable()
        {
            if (Target == null)
            {
                return false;
            }

            float distanceToTarget = Vector2.Distance(Target.position, Rb.position);
            if (distanceToTarget > DetectionRadius)
            {
                return false;
            }

            return true;
        }

        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }

        public virtual void Update()
        {
            targetIsReachable = TargetIsReachable();
        }

        public virtual void Hit(float hp)
        {
            HealthService.Hit(hp);
            if (HealthService.CurrentHp <= 0) Fsm.SetState<FsmStateDie>();
            else Fsm.SetState<FsmStateStun>();
        }

        public virtual void Heal(float hp)
        {
            HealthService.Heal(hp);
        }
    }
}