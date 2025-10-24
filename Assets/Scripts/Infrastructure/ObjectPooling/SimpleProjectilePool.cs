using System;
using Infrastructure.DIContainer;
using ProjectileLogic;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Infrastructure.ObjectPooling
{
    public class SimpleProjectilePool : IService
    {
        private readonly ObjectPool<SimpleProjectile> _projectilePool;
        
        private Action _returnToPool;

        public SimpleProjectilePool(GameObject projectilePrefab, int initialSize)
        {
            _projectilePool = new ObjectPool<SimpleProjectile>(
                () =>
                {
                    var projectile = Object.Instantiate(projectilePrefab).GetComponent<SimpleProjectile>();
                    return projectile;
                },
                projectile => projectile.gameObject.SetActive(true),
                projectile => projectile.gameObject.SetActive(false),
                projectile => Object.Destroy(projectile.gameObject),
                false,
                initialSize,
                initialSize * 2);
        }

        public SimpleProjectile Get(out Action returnToPool)
        {
            var projectile = _projectilePool.Get();
            returnToPool = () => _projectilePool.Release(projectile);
            
            return projectile;
        }

        public void ReleaseProjectile(SimpleProjectile projectile)
        {
            _projectilePool.Release(projectile);
        }

        public void Dispose()
        {
            _projectilePool.Clear();
        }
    }
}