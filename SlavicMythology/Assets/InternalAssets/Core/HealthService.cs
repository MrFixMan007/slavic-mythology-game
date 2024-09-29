namespace Core.HealthService
{
    public interface IHealthService
    {
        float CurrentHp { get; }
        float MaxHp { get; }
        void Heal(float hp);
        void Hit(float hp);
    }
    
    public class HealthService : IHealthService
    {
        private float _currentHp;
        private float _maxHp;

        public float CurrentHp => _currentHp;
        public float MaxHp => _maxHp;

        public void Heal(float hp)
        {
            _currentHp += hp;
            if (_currentHp > _maxHp)
            {
                _currentHp = _maxHp;
            }
        }

        public void Hit(float hp)
        {
            if (_currentHp > 0)
            {
                _currentHp -= hp;
            }
        }

        public HealthService(float hp)
        {
            _maxHp = hp;
            _currentHp = hp;
        }
    }
}