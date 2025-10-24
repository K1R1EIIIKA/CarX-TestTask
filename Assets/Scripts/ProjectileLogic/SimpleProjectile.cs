using MonstersLogic;
using UnityEngine;

namespace ProjectileLogic
{
    public class SimpleProjectile : BaseProjectile
    {
        private Monster _target;

        public override void Launch(Monster target)
        {
            base.Launch(target);
            _target = target;
        }

        protected override void Move()
        {
            if (_target == null)
            {
                ReleaseToPool();
                return;
            }

            var direction = _target.transform.position - transform.position;
            var distanceThisFrame = _speed * Time.deltaTime;

            if (direction.sqrMagnitude <= distanceThisFrame * distanceThisFrame)
            {
                transform.position = _target.transform.position;
            }
            else
            {
                transform.position += direction.normalized * distanceThisFrame;
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }
    }
}
