using Infrastructure.DIContainer;
using ProjectileLogic;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IProjectileFactory : IService
    {
        SimpleProjectile CreateSimpleProjectile(Vector3 position, Quaternion rotation);
        CannonProjectile CreateCannonProjectile(Vector3 position, Quaternion rotation);
        MortarProjectile CreateMortarProjectile(Vector3 position, Quaternion rotation);
    }
}