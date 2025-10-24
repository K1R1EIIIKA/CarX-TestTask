using UnityEngine;

namespace Infrastructure.Configs
{
    public abstract class ProjectileConfig : ScriptableObject
    {
        [SerializeField] protected float speed = 0f;
        [SerializeField] protected int damage = 0;
        [SerializeField] private float _lifetime = 5f;
        
        [SerializeField] protected GameObject projectilePrefab;

        public float Speed => speed;
        public int Damage => damage;
        public float Lifetime => _lifetime;
        public GameObject ProjectilePrefab => projectilePrefab;

        protected virtual void OnValidate()
        {
            speed = Mathf.Max(0f, speed);
            damage = Mathf.Max(0, damage);
            _lifetime = Mathf.Max(0f, _lifetime);
        }
    }
}
