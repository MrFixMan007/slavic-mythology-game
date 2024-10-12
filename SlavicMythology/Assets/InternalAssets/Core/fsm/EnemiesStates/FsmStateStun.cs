using Core.Battle;
using Pathfinding;
using UnityEngine;

namespace FSM.States
{
    public abstract class FsmStateStun : FsmStateEnemy
    {
        protected float StanTime;
        protected float StanTick;

        protected FsmStateStun(FsmEnemy fsm, Transform target, Path path, Rigidbody2D rb, float detectionRadius,
            float hp, float stanTime, Animator animator) : base(fsm: fsm, target: target, path: path, rb: rb,
            detectionRadius: detectionRadius,
            hp: hp, animator: animator)
        {
            StanTime = stanTime;
        }

        public override void Enter()
        {
        }

        public override void Exit()
        {
            StanTick = 0;
        }

        public override void Update()
        {
            base.Update();
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
            float hp, float stanTime, ISimpleBattleService simpleBattleService, Animator animator) : base(fsm: fsm,
            target: target, path: path,
            rb: rb, detectionRadius: detectionRadius,
            hp: hp, stanTime: stanTime, animator: animator)
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