using Infrastructure.DIContainer;
using ProjectileLogic;
using UnityEngine;
using UnityEngine.Pool;

namespace Infrastructure.ObjectPooling
{
    public class MortarProjectilePool : IService
    {
        private readonly ObjectPool<MortarProjectile> _pool;

        public MortarProjectilePool(MortarProjectile projectilePrefab, int initialSize)
        {
            _pool = new ObjectPool<MortarProjectile>(
                createFunc: () => Object.Instantiate(projectilePrefab),
                actionOnGet: projectile => projectile.gameObject.SetActive(true),
                actionOnRelease: projectile => projectile.gameObject.SetActive(false),
                actionOnDestroy: projectile => Object.Destroy(projectile.gameObject),
                collectionCheck: false,
                defaultCapacity: initialSize,
                maxSize: 100
            );
        }

        public MortarProjectile Get()
        {
            return _pool.Get();
        }

        public void Release(MortarProjectile projectile)
        {
            _pool.Release(projectile);
        }

        public void Dispose()
        {
            _pool.Clear();
        }
    }
}