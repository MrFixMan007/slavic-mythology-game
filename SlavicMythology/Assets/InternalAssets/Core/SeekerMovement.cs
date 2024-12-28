using Pathfinding;
using UnityEngine;

namespace Movement
{
    public class SeekerMovement
    {
        private readonly Rigidbody2D _rb;
        private readonly Transform _target;

        private readonly Seeker _seeker;
        private const float NextWaypointDistance = 1f;
        public Path Path { get; private set; }
        private int _currentWayPoint;

        private float _moveSpeed;

        public SeekerMovement(Rigidbody2D rb, Transform target, Seeker seeker, float moveSpeed)
        {
            _rb = rb;
            _target = target;
            _seeker = seeker;
            _moveSpeed = moveSpeed;
        }

        public void Update()
        {
            if (_seeker.IsDone())
                _seeker.StartPath(_rb.position, _target.position, OnPathComplete);
        }

        private void OnPathComplete(Path p)
        {
            if (!p.error)
            {
                Path = p;
                _currentWayPoint = 0;
            }
        }

        public void MoveChar()
        {
            if (Path != null)
            {
                // Vector2 direction = ((Vector2)Path.vectorPath[_currentWayPoint] - _rb.position).normalized;
                // Vector2 movePosition = _rb.position + (direction * _moveSpeed * Time.deltaTime);
                // _rb.MovePosition(movePosition);
                //
                // float distance = Vector2.Distance(_rb.position, Path.vectorPath[_currentWayPoint]);
                // if (distance < NextWaypointDistance && _currentWayPoint < Path.vectorPath.Count - 1)
                // {
                //     _currentWayPoint++;
                // }
                // ������� ����������� � ��������� �����
                Vector2 direction = ((Vector2)Path.vectorPath[_currentWayPoint] - _rb.position).normalized;

                // ��������� �������� ����� velocity ��� �������� � ������ �����������
                _rb.linearVelocity = direction * _moveSpeed;

                // �������� ���������� �� ������� ����� ����
                float distance = Vector2.Distance(_rb.position, Path.vectorPath[_currentWayPoint]);

                // ���� ���������� ������ ���������� � ������� ����� �� ���������, ��������� � ���������
                if (distance < NextWaypointDistance && _currentWayPoint < Path.vectorPath.Count - 1)
                {
                    _currentWayPoint++;
                }
            }
        }

        public void StopChar()
        {
            _rb.linearVelocity = Vector2.zero;
        }
    }
}