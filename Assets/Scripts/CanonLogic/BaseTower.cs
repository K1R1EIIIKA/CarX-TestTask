using Infrastructure.Asset;
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
        protected IAssetDatabase assetDatabase;

        public void Initialize(IProjectileFactory factory, IAssetDatabase assetDb)
        {
            projectileFactory = factory;
            assetDatabase = assetDb;
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
            
            if (target != null)
            {
                Aim(target);
            }
        }

        private Transform GetNearestTarget()
        {
            Monster[] monsters = FindObjectsOfType<Monster>();
            Transform nearest = null;
            float nearestDist = float.MaxValue;
            foreach (Monster monster in monsters)
            {
                if (monster == null)
                    continue;

                float dist = Vector3.Distance(transform.position, monster.transform.position);
                if (dist <= _range && dist < nearestDist)
                {
                    nearest = monster.transform;
                    nearestDist = dist;
                }
            }
            return nearest;
        }

        protected abstract void Aim(Transform target);
        protected abstract void Shoot(Transform target);
    }
}
