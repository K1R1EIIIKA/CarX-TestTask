using CanonLogic;
using Infrastructure.Asset;
using Infrastructure.ObjectPooling;
using ProjectileLogic;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class ProjectileFactory : IProjectileFactory
    {
        private readonly SimpleProjectilePool _simpleProjectilePool;
        private readonly CannonProjectilePool _cannonProjectilePool;
        private readonly MortarProjectilePool _mortarProjectilePool;
        
        private readonly IAssetDatabase _assetDatabase;

        public ProjectileFactory(SimpleProjectilePool simpleProjectilePool,
            CannonProjectilePool cannonProjectilePool,
            MortarProjectilePool mortarProjectilePool,
            IAssetDatabase assetDatabase)
        {
            _simpleProjectilePool = simpleProjectilePool;
            _cannonProjectilePool = cannonProjectilePool;
            _mortarProjectilePool = mortarProjectilePool;
            _assetDatabase = assetDatabase;
        }

        public SimpleProjectile CreateSimpleProjectile(Vector3 position, Quaternion rotation)
        {
            var projectile = _simpleProjectilePool.Get(out var returnToPoolAction);
            
            var config = _assetDatabase.ProjectileConfig(ProjectileType.Simple);
            projectile.Initialize(config, returnToPoolAction);
            projectile.transform.SetPositionAndRotation(position, rotation);
            
            return projectile;
        }

        public CannonProjectile CreateCannonProjectile(Vector3 position, Quaternion rotation)
        {
            var projectile = _cannonProjectilePool.Get(out var returnToPoolAction);
            
            var config = _assetDatabase.ProjectileConfig(ProjectileType.Cannon);
            projectile.Initialize(config, returnToPoolAction);
            projectile.transform.SetPositionAndRotation(position, rotation);
            
            return projectile;
        }

        public MortarProjectile CreateMortarProjectile(Vector3 position, Quaternion rotation)
        {
            var projectile = _mortarProjectilePool.Get(out var returnToPoolAction);
            
            var config = _assetDatabase.ProjectileConfig(ProjectileType.Mortar);
            projectile.Initialize(config, returnToPoolAction);
            projectile.transform.SetPositionAndRotation(position, rotation);
            
            return projectile;
        }
        public BaseProjectile CreateProjectile(ProjectileType projectileType, Vector3 shootPointPosition, Quaternion transformRotation)
        {
            return projectileType switch
            {
                ProjectileType.Simple => CreateSimpleProjectile(shootPointPosition, transformRotation),
                ProjectileType.Cannon => CreateCannonProjectile(shootPointPosition, transformRotation),
                ProjectileType.Mortar => CreateMortarProjectile(shootPointPosition, transformRotation),
                _ => null,
            };
        }
    }
}