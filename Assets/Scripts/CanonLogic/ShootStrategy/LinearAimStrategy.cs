using Infrastructure.Configs;
using UnityEngine;

namespace CanonLogic.ShootStrategy
{
    [System.Serializable]
    public class LinearAimStrategy : IAimStrategy
    {
        public Vector3 CalculateAimDirection(Vector3 shootPos, Vector3 targetPos, Vector3 targetVel, ProjectileConfig config)
        {
            var toTarget = targetPos - shootPos;
            var a = Vector3.Dot(targetVel, targetVel) - config.Speed * config.Speed;
            var b = 2f * Vector3.Dot(targetVel, toTarget);
            var c = Vector3.Dot(toTarget, toTarget);
            const float eps = 0.001f;

            bool TryFindImpactTime(out float t)
            {
                t = float.NaN;
                if (Mathf.Abs(a) < eps)
                {
                    if (Mathf.Abs(b) < eps) return false;
                    t = -c / b;
                    return t > 0f;
                }

                var disc = b * b - 4f * a * c;
                if (disc < 0f) return false;

                var sqrtDisc = Mathf.Sqrt(disc);
                var t1 = (-b + sqrtDisc) / (2f * a);
                var t2 = (-b - sqrtDisc) / (2f * a);

                if (t1 > 0f && t2 > 0f)
                    t = Mathf.Min(t1, t2);
                else
                    t = Mathf.Max(t1, t2);

                return t > 0f;
            }

            if (!TryFindImpactTime(out float impactTime))
            {
                return (targetPos - shootPos).normalized;
            }

            var predictedPos = targetPos + targetVel * impactTime;
            return (predictedPos - shootPos).normalized;
        }
    }
}
