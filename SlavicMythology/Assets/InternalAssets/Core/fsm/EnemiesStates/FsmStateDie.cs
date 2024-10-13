using Core;
using FSM.Animation;
using Pathfinding;
using UnityEngine;

namespace FSM.States
{
    public abstract class FsmStateDie : FsmStateEnemy
    {
        protected IDestroyableGameObject GameObject;

        protected FsmStateDie(FsmEnemy fsm, Transform target, Path path, Rigidbody2D rb, float detectionRadius,
            float hp, IDestroyableGameObject gameObject, Animator animator) : base(
            fsm: fsm, target: target, path: path, rb: rb,
            detectionRadius: detectionRadius,
            hp: hp, animator: animator)
        {
            GameObject = gameObject;
        }

        public override void Enter()
        {
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
        }
    }

    public class FsmStateForcedPushDie : FsmStateDie
    {
        public float Force;
        public float MinSpeed;
        public float DestroyDelay;

        private float _timeSincePush;
        private Vector2 _direction;

        private float _velocityFlag = 1f;

        public FsmStateForcedPushDie(FsmEnemy fsm, Transform target, Path path, Rigidbody2D rb,
            float detectionRadius, float hp, IDestroyableGameObject gameObject, float force, float minSpeed,
            float destroyDelay, Animator animator) : base(
            fsm: fsm, target: target, path: path, rb: rb,
            detectionRadius: detectionRadius,
            hp: hp, gameObject: gameObject, animator: animator)
        {
            Force = force;
            MinSpeed = minSpeed;
            DestroyDelay = destroyDelay;
        }

        public override void Enter()
        {
            Vector2 vector = Rb.velocity;
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

            _direction = (Rb.position - (Vector2)Target.position).normalized;
            Rb.AddForce(_direction * Force, ForceMode2D.Impulse);
        }

        public override void Update()
        {
            if (Rb.velocity.magnitude < MinSpeed)
            {
                GameObject.Destroy();
            }

            _timeSincePush += Time.deltaTime;
            if (_timeSincePush >= DestroyDelay)
            {
                GameObject.Destroy();
            }
        }
    }
}