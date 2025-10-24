using UnityEngine;

namespace Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "SimpleProjectileConfig", menuName = "Configs/SimpleProjectileConfig", order = 0)]
    public class SimpleProjectileConfig : ProjectileConfig
    {
        private void OnEnable()
        {
            if (speed <= 0f) speed = 0.2f;
            if (damage <= 0) damage = 10;
        }
    }
}
