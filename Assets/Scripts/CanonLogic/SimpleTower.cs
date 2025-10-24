using MonstersLogic;
using UnityEngine;

namespace CanonLogic
{
    public class SimpleTower : BaseTower
    {
        protected override void Shoot(Transform targetTransform)
        {
            var target = targetTransform.GetComponent<Monster>();

            var projectile = projectileFactory.CreateProjectile(projectileType, shootPoint.position, transform.rotation);
            if (projectile != null)
            {
                projectile.Launch(target);
            }
        }
    }
}
