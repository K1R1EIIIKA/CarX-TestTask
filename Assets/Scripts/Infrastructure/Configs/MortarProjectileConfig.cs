using UnityEngine;

namespace Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "MortarProjectileConfig", menuName = "Configs/MortarProjectileConfig", order = 2)]
    public class MortarProjectileConfig : ProjectileConfig
    {
        [SerializeField] private float _gravity = 9.81f;
        [SerializeField] private float _launchAngle = 45f;
        
        public float Gravity => _gravity;
        public float LaunchAngle => _launchAngle;
        
        private void OnEnable()
        {
            if (speed <= 0f) speed = 0.1f;
            if (damage <= 0) damage = 30;
            if (_gravity <= 0f) _gravity = 9.81f;
            if (_launchAngle <= 0f) _launchAngle = 45f;
        }
    }
}
