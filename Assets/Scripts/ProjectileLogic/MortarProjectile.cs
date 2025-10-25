using MonstersLogic;
using UnityEngine;

namespace ProjectileLogic
{
    public class MortarProjectile : BaseProjectile
    {
        private Vector3 _velocity;
        [SerializeField] private float _gravity = -20f;  // Сила гравитации (настраивай в префабе)
        [SerializeField] private float _launchAngle = 45f; // Угол стрельбы вверх (по умолчанию)

        protected override void Move()
        {
            // ✅ Гравитация действует только по Y!
            _velocity.y += _gravity * Time.deltaTime;
            transform.position += _velocity * Time.deltaTime;
        }

        public override void Launch(Monster target)
        {
            base.Launch(target);

            Vector3 shootPos = transform.position;
            Vector3 targetPos = target.transform.position;
            Vector3 targetVel = target.Velocity;

            // ✅ Рассчитываем оптимальную траекторию с упреждением
            if (CalculateParabolicLaunch(shootPos, targetPos, targetVel, out Vector3 launchVelocity))
            {
                _velocity = launchVelocity;
            }
            else
            {
                // Fallback: простая дуга
                _velocity = SimpleArcLaunch(shootPos, targetPos);
            }

            // ✅ Поворачиваем снаряд в направлении начальной скорости
            transform.rotation = Quaternion.LookRotation(_velocity);
        }

        // ✅ ОСНОВНОЙ РАСЧЁТ: Параболическое упреждение
        private bool CalculateParabolicLaunch(Vector3 shootPos, Vector3 targetPos, Vector3 targetVel, out Vector3 velocity)
        {
            Vector3 deltaPos = targetPos - shootPos;
            float horizontalDist = new Vector3(deltaPos.x, 0, deltaPos.z).magnitude;

            if (horizontalDist < 0.1f)
            {
                velocity = Vector3.zero;
                return false;
            }

            // Итеративный поиск оптимального времени полёта
            float minTime = 0.1f;
            float maxTime = 5f;
            float bestTime = -1f;
            float minError = float.MaxValue;

            for (int i = 0; i < 50; i++) // 50 итераций достаточно
            {
                float t = minTime + (maxTime - minTime) * i / 49f;
                
                // Предсказанная позиция цели
                Vector3 predictedTarget = targetPos + targetVel * t;
                float predictedHorizontalDist = new Vector3(predictedTarget.x - shootPos.x, 0, predictedTarget.z - shootPos.z).magnitude;
                
                // Горизонтальная скорость = дистанция / время
                float Vx = predictedHorizontalDist / t;
                
                // Вертикальная скорость для достижения высоты
                float deltaY = predictedTarget.y - shootPos.y;
                float Vy = (deltaY - 0.5f * _gravity * t * t) / t;
                
                if (Vy < 0) continue; // Снаряд не должен падать сразу

                // Проверяем, достигает ли снаряд цели
                float finalY = shootPos.y + Vy * t + 0.5f * _gravity * t * t;
                float error = Mathf.Abs(finalY - predictedTarget.y);
                
                if (error < minError)
                {
                    minError = error;
                    bestTime = t;
                }
            }

            if (bestTime > 0 && minError < 1f) // Допустимая ошибка 1 юнит
            {
                float t = bestTime;
                Vector3 predictedTarget = targetPos + targetVel * t;
                float predictedHorizontalDist = new Vector3(predictedTarget.x - shootPos.x, 0, predictedTarget.z - shootPos.z).magnitude;
                
                float Vx = predictedHorizontalDist / t;
                float Vy = (predictedTarget.y - shootPos.y - 0.5f * _gravity * t * t) / t;
                
                // Направление
                Vector3 horizontalDir = new Vector3(deltaPos.x, 0, deltaPos.z).normalized;
                velocity = horizontalDir * Vx + Vector3.up * Vy;
                
                return true;
            }

            velocity = Vector3.zero;
            return false;
        }

        // ✅ Fallback: Простая параболическая дуга
        private Vector3 SimpleArcLaunch(Vector3 shootPos, Vector3 targetPos)
        {
            Vector3 delta = targetPos - shootPos;
            float horizontalDist = new Vector3(delta.x, 0, delta.z).magnitude;
            
            // Время полёта для красивой дуги
            float flightTime = horizontalDist / _speed * 1.5f;
            
            // Вертикальная скорость
            float peakHeight = horizontalDist * 0.5f; // Высота дуги = 50% дистанции
            float Vy = Mathf.Sqrt(2f * Mathf.Abs(_gravity) * peakHeight);
            
            // Горизонтальная скорость
            float Vx = horizontalDist / flightTime;
            
            Vector3 horizontalDir = new Vector3(delta.x, 0, delta.z).normalized;
            return horizontalDir * Vx + Vector3.up * Vy;
        }
    }
}