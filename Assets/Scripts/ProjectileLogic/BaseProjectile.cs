using MonstersLogic;
using UnityEngine;

namespace ProjectileLogic
{
    public abstract class BaseProjectile : MonoBehaviour
    {
        [SerializeField] protected float _speed = 0.2f;
        [SerializeField] private int _damage = 10;

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