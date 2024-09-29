using Pathfinding;
using UnityEngine;

namespace FSM.States
{
    public abstract class FsmStatePeaceful : FsmStateEnemy
    {
        private float _distanceToTarget;

        protected FsmStatePeaceful(FsmEnemy fsm, Transform target, Path path, Rigidbody2D rb,
            float detectionRadius,
            float hp) : base(fsm: fsm, target: target, path: path, rb: rb, detectionRadius: detectionRadius,
            hp: hp)
        {
        }

        public override void Enter()
        {
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
            base.Update();
            if (targetIsReachable)
            {
                Fsm.SetState<FsmStateAggressive>();
            }
        }
    }

    public class FsmStateIdle : FsmStatePeaceful
    {
        public FsmStateIdle(FsmEnemy fsm, Transform target, Path path, Rigidbody2D rb, float detectionRadius,
            float hp) : base(fsm: fsm, target: target, path: path, rb: rb, detectionRadius: detectionRadius,
            hp: hp)
        {
        }
    }
}