using UnityEngine;

namespace Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "CannonProjectileConfig", menuName = "Configs/CannonProjectileConfig", order = 1)]
    public class CannonProjectileConfig : ProjectileConfig
    {
        private void OnEnable()
        {
            if (speed <= 0f) speed = 0.3f;
            if (damage <= 0) damage = 20;
        }
    }
}
