using MonstersLogic;
using UnityEngine;

namespace ProjectileLogic
{
    public abstract class BaseProjectile : MonoBehaviour
    {
        protected float _speed = 0.2f;
        private int _damage = 10;

        public void Initialize(float speed, int damage)
        {
            _speed = speed;
            _damage = damage;
        }

        private void Update()
        {
            Move();
        }

        private void OnTriggerEnter(Collider other)
        {
            var monster = other.gameObject.GetComponent<Monster>();
            if (monster == null)
                return;

            monster.TakeDamage(_damage);
            Destroy(gameObject);
        }

        protected abstract void Move();

        public abstract void Launch(Monster target);
    }
}