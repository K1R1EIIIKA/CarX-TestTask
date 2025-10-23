using MonstersLogic;
using ProjectileLogic;
using UnityEngine;

public class SimpleTower : BaseTower
{
    protected override void Shoot(Transform targetTransform)
    {
        Monster target = targetTransform.GetComponent<Monster>();
        if (target == null) return;

        var projectileObject = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
        var projectile = projectileObject.GetComponent<SimpleProjectile>();
        if (projectile != null)
        {
            projectile.Launch(target);
        }
    }
}