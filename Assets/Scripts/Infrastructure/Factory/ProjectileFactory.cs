using Infrastructure.ObjectPooling;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class ProjectileFactory : IProjectileFactory
    {
        private readonly SimpleProjectilePool _simpleProjectilePool;
        private readonly CannonProjectilePool _cannonProjectilePool;
        private readonly MortarProjectilePool _mortarProjectilePool;

        public ProjectileFactory(SimpleProjectilePool simpleProjectilePool,
            CannonProjectilePool cannonProjectilePool,
            MortarProjectilePool mortarProjectilePool)
        {
            _simpleProjectilePool = simpleProjectilePool;
            _cannonProjectilePool = cannonProjectilePool;
            _mortarProjectilePool = mortarProjectilePool;
        }

        public GameObject CreateSimpleProjectile(Vector3 position, Quaternion rotation)
        {
            throw new System.NotImplementedException();
        }

        public GameObject CreateCannonProjectile(Vector3 position, Quaternion rotation)
        {
            throw new System.NotImplementedException();
        }

        public GameObject CreateMortarProjectile(Vector3 position, Quaternion rotation)
        {
            throw new System.NotImplementedException();
        }
    }
}