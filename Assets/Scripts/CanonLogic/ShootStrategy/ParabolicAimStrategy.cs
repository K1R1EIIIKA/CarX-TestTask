using Infrastructure.Configs;
using UnityEngine;

namespace CanonLogic.ShootStrategy
{
    [System.Serializable]
    public class ParabolicAimStrategy : IAimStrategy
    {
        public Vector3 CalculateAimDirection(Vector3 shootPos, Vector3 targetPos, Vector3 targetVel, ProjectileConfig config)
        {
            float speedXZ = config.Speed;
            Vector3 deltaPos = targetPos - shootPos;
            Vector3 deltaVel = targetVel;

            float horizontalDistance = new Vector3(deltaPos.x, 0, deltaPos.z).magnitude;
            float flightTime = horizontalDistance / speedXZ;

            if (flightTime <= 0)
                return (targetPos - shootPos).normalized;

            float predictedTargetY = targetPos.y + deltaVel.y * flightTime;
            float targetHeightDiff = predictedTargetY - shootPos.y;

            var mortarConfig = config as MortarProjectileConfig;
            float Vy = (targetHeightDiff - 0.5f * mortarConfig.Gravity * flightTime * flightTime) / flightTime;

            Vector3 horizontalDir = new Vector3(deltaPos.x, 0, deltaPos.z).normalized;
            Vector3 launchDir = horizontalDir + Vector3.up * (Vy / speedXZ);

            return launchDir.normalized;
        }
    }
}
