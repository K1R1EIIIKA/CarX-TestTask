using CanonLogic.ShootStrategy;
using Infrastructure.Asset;
using Infrastructure.Factory;
using MonstersLogic;
using UnityEngine;

namespace CanonLogic
{
    public class CannonTower : BaseTower
    {
        [SerializeField] private Transform _horizontalPivot;
        [SerializeField] private Transform _verticalPivot;
        [SerializeField] private float _aimSpeed = 5f;

        // ✅ Стратегии наведения
        private IAimStrategy _aimStrategy;

        protected override void Aim(Transform targetTransform)
        {
            var target = targetTransform.GetComponent<Monster>();
            if (target == null) return;

            Vector3 shootPos = shootPoint.position;
            Vector3 targetPos = target.transform.position;
            Vector3 targetVel = target.Velocity;

            var config = assetDatabase.ProjectileConfig(projectileType); // Предполагаем такой метод
            
            // ✅ Используем стратегию
            Vector3 aimDirection = _aimStrategy.CalculateAimDirection(shootPos, targetPos, targetVel, config);
            RotateTurretTowards(aimDirection);
        }

        protected override void Shoot(Transform targetTransform)
        {
            var target = targetTransform.GetComponent<Monster>();
            if (target == null) return;

            var projectile = projectileFactory.CreateProjectile(projectileType, shootPoint.position, shootPoint.rotation);
            if (projectile != null)
            {
                projectile.Launch(target);
            }
        }

        public override void Initialize(IProjectileFactory factory, IAssetDatabase assetDb)
        {
            base.Initialize(factory, assetDb);
            
            // ✅ Инициализируем стратегию по типу снаряда
            _aimStrategy = projectileType switch
            {
                ProjectileType.Cannon => new LinearAimStrategy(),
                ProjectileType.Mortar => new ParabolicAimStrategy(),
                _ => new LinearAimStrategy(),
            };
        }

        private void RotateTurretTowards(Vector3 direction)
        {
            if (direction == Vector3.zero) return;

            // Горизонтальный поворот (Y)
            if (_horizontalPivot != null)
            {
                Quaternion horizontalRot = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                _horizontalPivot.rotation = Quaternion.Slerp(_horizontalPivot.rotation, horizontalRot, Time.deltaTime * _aimSpeed);
            }

            // Вертикальный поворот (X)
            if (_verticalPivot != null && _horizontalPivot != null)
            {
                Vector3 localDirection = Quaternion.Inverse(_horizontalPivot.rotation) * direction;
                float desiredAngle = Mathf.Atan2(localDirection.y, localDirection.z) * Mathf.Rad2Deg;
                desiredAngle = Mathf.Clamp(desiredAngle, -80f, 80f);

                Quaternion verticalRot = Quaternion.Euler(desiredAngle, 0, 0);
                _verticalPivot.localRotation = Quaternion.Slerp(_verticalPivot.localRotation, verticalRot, Time.deltaTime * _aimSpeed);
            }
        }
    }

}