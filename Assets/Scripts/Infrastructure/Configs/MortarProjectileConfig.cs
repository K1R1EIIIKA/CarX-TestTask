using UnityEngine;

namespace Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "MortarProjectileConfig", menuName = "Configs/MortarProjectileConfig", order = 2)]
    public class MortarProjectileConfig : ProjectileConfig
    {
        private void OnEnable()
        {
            if (speed <= 0f) speed = 0.1f;
            if (damage <= 0) damage = 30;
        }
    }
}
