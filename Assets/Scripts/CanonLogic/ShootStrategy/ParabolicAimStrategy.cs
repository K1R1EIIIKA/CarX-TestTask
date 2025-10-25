using Infrastructure.Configs;
using UnityEngine;

namespace CanonLogic.ShootStrategy
{
    [System.Serializable]
    public class ParabolicAimStrategy : IAimStrategy
    {
        private const int MaxIterations = 30;
        private const float SpeedTolerance = 0.15f;

        public Vector3 CalculateAimDirection(Vector3 shootPos, Vector3 targetPos, Vector3 targetVel, ProjectileConfig config)
        {
            var mortarConfig = config as MortarProjectileConfig;
            if (mortarConfig == null) return (targetPos - shootPos).normalized;

            var speed = mortarConfig.Speed;
            var gravity = -Mathf.Abs(mortarConfig.Gravity);

            if (!TryPredictImpact(shootPos, targetPos, targetVel, speed, gravity,
                out float impactTime, out Vector3 predictedPos))
            {
                return (targetPos - shootPos).normalized;
            }

            Vector3 toTarget = predictedPos - shootPos;
            var distXZ = new Vector2(toTarget.x, toTarget.z).magnitude;
            var dy = toTarget.y;

            var v0x = distXZ / impactTime;
            var v0y = (dy - 0.5f * gravity * impactTime * impactTime) / impactTime;

            Vector3 dirXZ = new Vector3(toTarget.x, 0, toTarget.z).normalized;
            Vector3 launchDirection = dirXZ * v0x + -Vector3.up * v0y;
            
            return launchDirection.normalized; 
        }

        private bool TryPredictImpact(
            Vector3 shootPos,
            Vector3 targetPos,
            Vector3 targetVel,
            float speed,
            float gravity,
            out float impactTime,
            out Vector3 predictedPos)
        {
            impactTime = 0f;
            predictedPos = Vector3.zero;

            var tLow = 0.1f;
            var tHigh = 20f;
            var bestT = -1f;
            var bestError = float.MaxValue;

            for (int i = 0; i < MaxIterations; i++)
            {
                float tMid = (tLow + tHigh) * 0.5f;
                Vector3 futurePos = targetPos + targetVel * tMid;
                Vector3 toFuture = futurePos - shootPos;

                var distXZ = new Vector2(toFuture.x, toFuture.z).magnitude;
                var dy = toFuture.y;

                var v0x = distXZ / tMid;
                var v0y = (dy - 0.5f * gravity * tMid * tMid) / tMid;
                var requiredSpeed = Mathf.Sqrt(v0x * v0x + v0y * v0y);

                var error = Mathf.Abs(requiredSpeed - speed);

                if (error < bestError)
                {
                    bestError = error;
                    bestT = tMid;
                    predictedPos = futurePos;
                }

                if (requiredSpeed > speed) tHigh = tMid;
                else tLow = tMid;
            }

            if (bestT > 0f && bestError <= speed * SpeedTolerance)
            {
                impactTime = bestT;
                return true;
            }

            return false;
        }
    }
}