using System;
using System.Collections;
using Infrastructure.Configs;
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

        public virtual void Initialize(ProjectileConfig config, Action returnToPoolAction)
        {
            _speed = config.Speed;
            _damage = config.Damage;
            _lifetime = config.Lifetime;
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
