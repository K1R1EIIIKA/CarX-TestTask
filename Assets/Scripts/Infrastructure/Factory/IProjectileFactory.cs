using Infrastructure.DIContainer;
using UnityEngine;

namespace Infrastructure.Factory
{
    public interface IProjectileFactory : IService
    {
        GameObject CreateSimpleProjectile(Vector3 position, Quaternion rotation);
        GameObject CreateCannonProjectile(Vector3 position, Quaternion rotation);
        GameObject CreateMortarProjectile(Vector3 position, Quaternion rotation);
    }
}