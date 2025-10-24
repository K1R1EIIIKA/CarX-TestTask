using Infrastructure.DIContainer;
using ProjectileLogic;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Infrastructure.ObjectPooling
{
    public class SimpleProjectilePool : IService
    {
        private readonly ObjectPool<SimpleProjectile> _projectilePool;

        public SimpleProjectilePool(SimpleProjectile projectilePrefab, int initialSize)
        {
            _projectilePool = new ObjectPool<SimpleProjectile>(
                () => Object.Instantiate(projectilePrefab),
                projectile => projectile.gameObject.SetActive(true),
                projectile => projectile.gameObject.SetActive(false),
                projectile => Object.Destroy(projectile.gameObject),
                false,
                initialSize,
                initialSize * 2);
        }

        public SimpleProjectile GetProjectile()
        {
            return _projectilePool.Get();
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