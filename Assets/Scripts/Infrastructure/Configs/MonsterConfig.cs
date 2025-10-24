using MonstersLogic;
using UnityEngine;

namespace Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "MonsterConfig", menuName = "Configs/MonsterConfig", order = 3)]
    public class MonsterConfig : ScriptableObject
    {
        [SerializeField] private int _health = 100;
        [SerializeField] private float _speed = 1f;
        [SerializeField] private Monster _monsterPrefab;

        public int Health => _health;
        public float Speed => _speed;
        public Monster MonsterPrefab => _monsterPrefab;
    }
}
