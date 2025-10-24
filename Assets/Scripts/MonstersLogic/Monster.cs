using UnityEngine;

namespace MonstersLogic
{
    public class Monster : MonoBehaviour, IDamageable
    {
        private float _speed = 0.1f;
        private int _maxHp = 30;

        private Transform _moveTarget;
        private const float ReachDistance = 0.3f;

        private int _hp;

        public int Hp => _hp;
        
        public void Initialize(float speed, int maxHp)
        {
            _speed = speed;
            _maxHp = maxHp;
            _hp = _maxHp;
        }

        public Vector3 Velocity
        {
            get
            {
                if (_moveTarget == null)
                    return Vector3.zero;

                var direction = (_moveTarget.position - transform.position).normalized;
                return direction * _speed / Time.deltaTime;
            }
        }

        private void Start()
        {
            _hp = _maxHp;
        }

        private void Update()
        {
            if (_moveTarget == null)
                return;

            if (Vector3.Distance(transform.position, _moveTarget.transform.position) <= ReachDistance)
            {
                Destroy(gameObject);
                return;
            }

            var direction = (_moveTarget.position - transform.position).normalized;
            var translation = direction * _speed;
            transform.Translate(translation);
        }

        public void SetMoveTarget(Transform target)
        {
            _moveTarget = target;
        }

        public void TakeDamage(int damage)
        {
            _hp -= damage;

            if (_hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}