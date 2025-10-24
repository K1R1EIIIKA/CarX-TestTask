using Infrastructure.Factory;
using MonstersLogic;
using UnityEngine;

namespace CanonLogic
{
    public abstract class BaseTower : MonoBehaviour
    {
        [SerializeField] private float _shootInterval = 0.5f;
        [SerializeField] private float _range = 4f;
        [SerializeField] protected Transform shootPoint;
        [SerializeField] protected ProjectileType projectileType;

        private float _lastShotTime = Mathf.NegativeInfinity;

        protected IProjectileFactory projectileFactory;

        public void Initialize(IProjectileFactory factory)
        {
            projectileFactory = factory;
        }

        protected virtual void Update()
        {
            if (Time.time - _lastShotTime < _shootInterval) return;

            var target = GetNearestTarget();
            if (target != null)
            {
                Shoot(target);
                _lastShotTime = Time.time;
            }
        }

        private Transform GetNearestTarget()
        {
            var hits = Physics.OverlapSphere(transform.position, _range);
            Transform nearestTarget = null;
            var nearestDistanceSqr = Mathf.Infinity;

            foreach (var hit in hits)
            {
                var distanceSqr = (hit.transform.position - transform.position).sqrMagnitude;
            
                if (!hit.TryGetComponent(out IDamageable _))
                    continue;
            
                if (distanceSqr < nearestDistanceSqr)
                {
                    nearestDistanceSqr = distanceSqr;
                    nearestTarget = hit.transform;
                }
            }

            return nearestTarget;
        }

        protected abstract void Shoot(Transform target);
    }
}
