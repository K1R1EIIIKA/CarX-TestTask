using UnityEngine;

namespace Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "MortarProjectileConfig", menuName = "Configs/MortarProjectileConfig", order = 2)]
    public class MortarProjectileConfig : ProjectileConfig
    {
        [SerializeField] private float _gravity = 9.81f;
        
        public float Gravity => _gravity;
        
        private void OnEnable()
        {
            if (speed <= 0f) speed = 0.1f;
            if (damage <= 0) damage = 30;
            if (_gravity <= 0f) _gravity = 9.81f;
        }
    }
}
