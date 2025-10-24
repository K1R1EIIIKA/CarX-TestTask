using Infrastructure.DIContainer;
using ProjectileLogic;
using UnityEngine;
using UnityEngine.Pool;

namespace Infrastructure.ObjectPooling
{
    public class CannonProjectilePool : IService
    {
        private readonly ObjectPool<CannonProjectile> _pool;
        public CannonProjectilePool(CannonProjectile projectilePrefab, int initialSize)
        {
            _pool = new ObjectPool<CannonProjectile>(
                createFunc: () => Object.Instantiate(projectilePrefab),
                actionOnGet: (projectile) => projectile.gameObject.SetActive(true),
                actionOnRelease: (projectile) => projectile.gameObject.SetActive(false),
                actionOnDestroy: (projectile) => Object.Destroy(projectile.gameObject),
                collectionCheck: false,
                defaultCapacity: initialSize,
                maxSize: 100
            );
        }

        public CannonProjectile Get()
        {
            return _pool.Get();
        }

        public void Release(CannonProjectile projectile)
        {
            _pool.Release(projectile);
        }

        public void Dispose()
        {
            _pool.Clear();
        }
    }
}