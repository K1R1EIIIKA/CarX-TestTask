using System;
using Infrastructure.DIContainer;
using ProjectileLogic;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Infrastructure.ObjectPooling
{
    public class CannonProjectilePool : IService
    {
        private readonly ObjectPool<CannonProjectile> _projectilePool;
        public CannonProjectilePool(GameObject projectilePrefab, int initialSize)
        {
            _projectilePool = new ObjectPool<CannonProjectile>(
                createFunc: () => Object.Instantiate(projectilePrefab).GetComponent<CannonProjectile>(),
                actionOnGet: (projectile) => projectile.gameObject.SetActive(true),
                actionOnRelease: (projectile) => projectile.gameObject.SetActive(false),
                actionOnDestroy: (projectile) => Object.Destroy(projectile.gameObject),
                collectionCheck: false,
                defaultCapacity: initialSize,
                maxSize: 100
            );
        }

        public CannonProjectile Get(out Action returnToPool)
        {
            var projectile = _projectilePool.Get();
            returnToPool = () => _projectilePool.Release(projectile);
            
            return projectile;
        }

        public void Release(CannonProjectile projectile)
        {
            _projectilePool.Release(projectile);
        }

        public void Dispose()
        {
            _projectilePool.Clear();
        }
    }
}