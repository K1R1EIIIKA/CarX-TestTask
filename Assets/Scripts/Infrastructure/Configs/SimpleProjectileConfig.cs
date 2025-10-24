using UnityEngine;

namespace Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "SimpleProjectileConfig", menuName = "Configs/SimpleProjectileConfig", order = 0)]
    public class SimpleProjectileConfig : ScriptableObject
    {
        [SerializeField] protected float _speed = 0.2f;
        [SerializeField] private int _damage = 10;
        [SerializeField] private GameObject _projectilePrefab;

        public float Speed => _speed;
        public int Damage => _damage;
        public GameObject ProjectilePrefab => _projectilePrefab;
    }
}