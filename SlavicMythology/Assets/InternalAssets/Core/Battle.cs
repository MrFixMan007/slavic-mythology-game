using UnityEngine;

namespace Core.Battle
{
    public interface ISimpleBattleService
    {
        bool CanHit { get; }
        void Update();
        void Attack();
    }

    public interface ITwoTypesBattleService : ISimpleBattleService
    {
        bool CanStrongHit { get; }
        void StrongAttack();
    }

    public class SimpleMeleeAttackService : ISimpleBattleService
    {
        private float _meleeAttackCoolDown;
        private float _meleeAttackDamage;
        private Transform _target;
        private bool _readyToAttack = true;
        public bool CanHit => _readyToAttack;

        private float _tickLight;

        public SimpleMeleeAttackService(float meleeLightAttackCoolDown, float meleeAttackDamage, Transform target)
        {
            _meleeAttackCoolDown = meleeLightAttackCoolDown;
            _meleeAttackDamage = meleeAttackDamage;
            _target = target;
        }

        public void Update()
        {
            if (!_readyToAttack)
            {
                _tickLight += Time.deltaTime;
                if (_tickLight > _meleeAttackCoolDown)
                {
                    _readyToAttack = true;
                    _tickLight = 0;
                }
            }
        }

        public void Attack()
        {
            if (_target.TryGetComponent(out Health playerHealth))
            {
                playerHealth.TakeDamage((int)_meleeAttackDamage);
                _readyToAttack = false;
            }
        }
    }
}