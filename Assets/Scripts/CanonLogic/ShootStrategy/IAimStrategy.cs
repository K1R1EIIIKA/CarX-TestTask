using Infrastructure.Configs;
using UnityEngine;

namespace CanonLogic.ShootStrategy
{
    public interface IAimStrategy
    {
        Vector3 CalculateAimDirection(Vector3 shootPos, Vector3 targetPos, Vector3 targetVel, ProjectileConfig config);
    }
}
