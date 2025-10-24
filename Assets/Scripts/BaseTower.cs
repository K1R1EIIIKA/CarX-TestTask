using Infrastructure.Factory;
using MonstersLogic;
using ProjectileLogic;
using UnityEngine;

public abstract class BaseTower : MonoBehaviour
{
    [SerializeField] private float _shootInterval = 0.5f;
    [SerializeField] private float _range = 4f;

    private float _lastShotTime = Mathf.NegativeInfinity;

    protected IProjectileFactory projectileFactory;

    public void Initialize(IProjectileFactory factory)
    {
        projectileFactory = factory;
    }

    protected virtual void Update()
    {
        if (Time.time - _lastShotTime < _shootInterval) return;

        var target = GetNearestTarget();
        if (target != null)
        {
            Shoot(target);
            _lastShotTime = Time.time;
        }
    }

    private Transform GetNearestTarget()
    {
        Monster[] monsters = FindObjectsOfType<Monster>();
        Transform nearest = null;
        float nearestDist = float.MaxValue;
        foreach (Monster monster in monsters)
        {
            if (monster == null)
                continue;

            float dist = Vector3.Distance(transform.position, monster.transform.position);
            if (dist <= _range && dist < nearestDist)
            {
                nearest = monster.transform;
                nearestDist = dist;
            }
        }
        return nearest;
    }

    protected abstract void Shoot(Transform target);
}
