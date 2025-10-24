using System;
using System.Collections;
using MonstersLogic;
using UnityEngine;

namespace ProjectileLogic
{
    public abstract class BaseProjectile : MonoBehaviour
    {
        protected float _speed = 0.2f;
        private int _damage = 10;
        private float _lifetime = 5f;
        private Action _returnToPoolAction;

        public void Initialize(float speed, int damage, float lifetime, Action returnToPoolAction)
        {
            _speed = speed;
            _damage = damage;
            _lifetime = lifetime;
            _returnToPoolAction = returnToPoolAction;
        }

        private void Update()
        {
            Move();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out IDamageable damageable))
                return;

            damageable.TakeDamage(_damage);
            ReleaseToPool();
        }

        protected abstract void Move();

        public virtual void Launch(Monster target)
        {
            StartCoroutine(LifetimeCoroutine());
        }

        private IEnumerator LifetimeCoroutine()
        {
            yield return new WaitForSeconds(_lifetime);
            ReleaseToPool();
        }
        
        protected void ReleaseToPool()
        {
            _returnToPoolAction?.Invoke();
        }
    }
}
