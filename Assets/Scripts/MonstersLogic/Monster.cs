using UnityEngine;

namespace MonstersLogic
{
    public class Monster : MonoBehaviour
    {
        [SerializeField] private float _speed = 0.1f;
        [SerializeField] private int _maxHp = 30;

        private Transform _moveTarget;
        private const float ReachDistance = 0.3f;

        private int _hp;

        public int Hp => _hp;

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

            var translation = _moveTarget.transform.position - transform.position;
            if (translation.magnitude > _speed) translation = translation.normalized * _speed;

            transform.Translate(translation);
        }

        public void SetMoveTarget(Transform target)
        {
            _moveTarget = target;
        }
    }
}