using MonstersLogic;
using UnityEngine;

public class SimpleTower : BaseTower
{   
    protected override void Shoot(Transform targetTransform)
    {
        Monster target = targetTransform.GetComponent<Monster>();
        if (target == null) return;

        var projectile = projectileFactory.CreateSimpleProjectile(transform.position, transform.rotation);
        if (projectile != null)
        {
            projectile.Launch(target);
        }
    }
}
