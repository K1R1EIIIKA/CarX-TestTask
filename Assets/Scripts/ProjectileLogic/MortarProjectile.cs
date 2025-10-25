using System;
using Infrastructure.Configs;
using MonstersLogic;
using UnityEngine;

namespace ProjectileLogic
{
    public class MortarProjectile : BaseProjectile
    {
        private Vector3 _velocity;
        private float _gravity;
        private float _speed;

        public override void Initialize(ProjectileConfig config, Action returnToPoolAction)
        {
            base.Initialize(config, returnToPoolAction);

            var mortarConfig = config as MortarProjectileConfig;
            _gravity = -Mathf.Abs(mortarConfig.Gravity);
            _speed = mortarConfig.Speed;
        }

        protected override void Move()
        {
            _velocity.y += _gravity * Time.deltaTime;
            transform.position += _velocity * Time.deltaTime;

            if (_velocity.sqrMagnitude > 0.01f)
            {
                transform.rotation = Quaternion.LookRotation(_velocity);
            }
        }

        public override void Launch(Monster target)
        {
            base.Launch(target);

            Vector3 shootPos = transform.position;
            Vector3 targetPos = target.transform.position;
            Vector3 targetVel = target.Velocity;

            // Попытка найти точное время полёта
            if (!TryPredictImpact(
                shootPos,
                targetPos,
                targetVel,
                out float impactTime,
                out Vector3 predictedPos))
            {
                // Не можем попасть — возвращаем снаряд в пул
                ReleaseToPool();
                return;
            }

            // Успешно — запускаем
            LaunchWithVelocity(shootPos, predictedPos, impactTime);
        }

        private bool TryPredictImpact(
            Vector3 shootPos,
            Vector3 targetPos,
            Vector3 targetVel,
            out float impactTime,
            out Vector3 predictedPos)
        {
            impactTime = 0f;
            predictedPos = Vector3.zero;

            const int maxIterations = 30;
            const float timeTolerance = 0.02f;
            const float speedTolerance = 0.1f;

            float tLow = 0.1f;
            float tHigh = 20f; // Максимальное время

            float bestT = -1f;
            float bestError = float.MaxValue;

            // Бинарный поиск по времени
            for (int i = 0; i < maxIterations; i++)
            {
                float tMid = (tLow + tHigh) * 0.5f;
                Vector3 futurePos = targetPos + targetVel * tMid;
                Vector3 toFuture = futurePos - shootPos;

                float distXZ = new Vector2(toFuture.x, toFuture.z).magnitude;
                float dy = toFuture.y;

                if (!CalculateRequiredSpeed(distXZ, dy, tMid, out float requiredSpeed))
                    continue;

                float error = Mathf.Abs(requiredSpeed - _speed);

                if (error < bestError)
                {
                    bestError = error;
                    bestT = tMid;
                    predictedPos = futurePos;
                }

                // Сходимся к нужной скорости
                if (requiredSpeed > _speed)
                    tHigh = tMid;
                else
                    tLow = tMid;
            }

            // Проверяем, достаточно ли точно
            if (bestT > 0f && bestError <= _speed * 0.15f) // ±15%
            {
                impactTime = bestT;
                return true;
            }

            return false;
        }

        private bool CalculateRequiredSpeed(float distXZ, float dy, float t, out float requiredSpeed)
        {
            requiredSpeed = 0f;
            if (t <= 0f) return false;

            // v0x = distXZ / t
            // v0y = (dy - 0.5 * g * t^2) / t
            float v0x = distXZ / t;
            float v0y = (dy - 0.5f * _gravity * t * t) / t;

            requiredSpeed = Mathf.Sqrt(v0x * v0x + v0y * v0y);
            return true;
        }

        private void LaunchWithVelocity(Vector3 shootPos, Vector3 predictedPos, float impactTime)
        {
            Vector3 toTarget = predictedPos - shootPos;
            float distXZ = new Vector2(toTarget.x, toTarget.z).magnitude;
            float dy = toTarget.y;

            // Рассчитываем точную скорость
            float v0x = distXZ / impactTime;
            float v0y = (dy - 0.5f * _gravity * impactTime * impactTime) / impactTime;

            // Масштабируем, чтобы точно соответствовать _speed (если нужно)
            float currentSpeed = Mathf.Sqrt(v0x * v0x + v0y * v0y);
            if (currentSpeed > 0.001f)
            {
                float scale = _speed / currentSpeed;
                v0x *= scale;
                v0y *= scale;
            }

            Vector3 dirXZ = new Vector3(toTarget.x, 0, toTarget.z).normalized;
            _velocity = dirXZ * v0x + Vector3.up * v0y;

            transform.rotation = Quaternion.LookRotation(_velocity);
        }
    }
}