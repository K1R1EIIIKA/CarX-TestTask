using MonstersLogic;
using UnityEngine;

namespace ProjectileLogic
{
    public class CannonProjectile : BaseProjectile
    {
        private Vector3 _velocity;

        protected override void Move()
        {
            transform.position += _velocity * Time.deltaTime;
        }

        public override void Launch(Monster target)
        {
            base.Launch(target);

            var towerPos = transform.position;
            var targetPos = target.transform.position;
            var targetVel = target.Velocity;
            var toTarget = targetPos - towerPos;

            var a = Vector3.Dot(targetVel, targetVel) - _speed * _speed;
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

            void AimAndSetVelocity(Vector3 dir)
            {
                var direction = (dir.sqrMagnitude < 1e-6f) ? toTarget.normalized : dir.normalized;
                transform.rotation = Quaternion.LookRotation(direction);
                _velocity = direction * _speed;
            }

            if (!TryFindImpactTime(out float impactTime))
            {
                AimAndSetVelocity(toTarget);
                return;
            }

            var predictedPos = targetPos + targetVel * impactTime;
            AimAndSetVelocity(predictedPos - towerPos);
        }
    }
}
