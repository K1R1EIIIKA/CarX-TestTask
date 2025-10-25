using MonstersLogic;
using UnityEngine;

namespace CanonLogic {
    public class CannonTower : BaseTower {
        [SerializeField] private Transform _horizontalPivot;
        [SerializeField] private Transform _verticalPivot;
        [SerializeField] private float _rotationSpeed = 90f;

        protected override void Aim(Transform targetTransform) {
            var target = targetTransform.GetComponent<Monster>();
            if (target == null) return;
            Vector3 towerPos = shootPoint.position;
            Vector3 targetPos = target.transform.position;
            Vector3 targetVel = target.Velocity;
            Vector3 toTarget = targetPos - towerPos;

            var projectileSpeed = assetDatabase.ProjectileConfig(projectileType).Speed;
            var a = Vector3.Dot(targetVel, targetVel) - projectileSpeed * projectileSpeed;
            var b = 2f * Vector3.Dot(targetVel, toTarget);
            var c = Vector3.Dot(toTarget, toTarget);
            const float eps = 0.001f;

            var impactTime = 0f;
            var validSolution = false;
            if (Mathf.Abs(a) < eps) {
                if (Mathf.Abs(b) > eps) {
                    var t = -c / b;
                    if (t > 0f) { impactTime = t; validSolution = true; }
                }
            } else {
                var disc = b * b - 4f * a * c;
                if (disc >= 0f) {
                    var sqrt = Mathf.Sqrt(disc);
                    var t1 = (-b + sqrt) / (2f * a);
                    var t2 = (-b - sqrt) / (2f * a);
                    var t = (t1 > 0f && t2 > 0f) ? Mathf.Min(t1, t2) : Mathf.Max(t1, t2);
                    if (t > 0f) { impactTime = t; validSolution = true; }
                }
            }

            Vector3 predictedPos = validSolution 
                ? targetPos + targetVel * impactTime 
                : targetPos;

            Vector3 dir = predictedPos - towerPos;

            Vector3 flatDir = new Vector3(dir.x, 0f, dir.z);
            if (flatDir.sqrMagnitude > 1e-6f) {
                Quaternion desiredYaw = Quaternion.LookRotation(flatDir, Vector3.up);
                _horizontalPivot.rotation = Quaternion.RotateTowards(
                    _horizontalPivot.rotation,
                    desiredYaw,
                    _rotationSpeed * Time.deltaTime);
            }

            if (dir.sqrMagnitude > 1e-6f) {
                Quaternion fullRot = Quaternion.LookRotation(dir);
                Quaternion desiredPitch = Quaternion.Euler(fullRot.eulerAngles.x, 0f, 0f);
                _verticalPivot.localRotation = Quaternion.RotateTowards(
                    _verticalPivot.localRotation,
                    desiredPitch,
                    _rotationSpeed * Time.deltaTime);
            }
        }

        protected override void Shoot(Transform targetTransform) {
            var target = targetTransform.GetComponent<Monster>();
            if (target == null) return;

            var projectile = projectileFactory.CreateProjectile(projectileType, shootPoint.position, transform.rotation);
            if (projectile != null) {
                projectile.Launch(target);
            }
        }
    }
}
