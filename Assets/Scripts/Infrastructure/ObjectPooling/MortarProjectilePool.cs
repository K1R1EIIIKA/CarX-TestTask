using System;
using Infrastructure.DIContainer;
using ProjectileLogic;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Infrastructure.ObjectPooling
{
    public class MortarProjectilePool : IService
    {
        private readonly ObjectPool<MortarProjectile> _projectilePool;

        public MortarProjectilePool(GameObject projectilePrefab, int initialSize)
        {
            _projectilePool = new ObjectPool<MortarProjectile>(
                createFunc: () => Object.Instantiate(projectilePrefab).GetComponent<MortarProjectile>(),
                actionOnGet: projectile => projectile.gameObject.SetActive(true),
                actionOnRelease: projectile => projectile.gameObject.SetActive(false),
                actionOnDestroy: projectile => Object.Destroy(projectile.gameObject),
                collectionCheck: false,
                defaultCapacity: initialSize,
                maxSize: 100
            );
        }

        public MortarProjectile Get(out Action returnToPool)
        {
            var projectile = _projectilePool.Get();
            returnToPool = () => _projectilePool.Release(projectile);
            
            return projectile;
        }

        public void Release(MortarProjectile projectile)
        {
            _projectilePool.Release(projectile);
        }

        public void Dispose()
        {
            _projectilePool.Clear();
        }
    }
}