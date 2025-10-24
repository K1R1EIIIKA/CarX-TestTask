using Infrastructure.Factory;
using UnityEngine;

namespace MonstersLogic
{
    public class MonsterSpawner : MonoBehaviour
    {
        [SerializeField] private float _interval = 3;
        [SerializeField] private Transform _moveTarget;
        [SerializeField] private Monster _monsterPrefab;

        private float _lastSpawn = Mathf.NegativeInfinity;

        private IMonsterFactory _factory;

        void Update()
        {
            if (Time.time > _lastSpawn + _interval)
            {
                var monster = _factory.CreateMonster(transform.position, Quaternion.identity);
                monster.SetMoveTarget(_moveTarget);
                _lastSpawn = Time.time;
            }
        }

        public void EnableSpawner()
        {
            enabled = true;
        }

        public void DisableSpawner()
        {
            enabled = false;
        }

        public void Initialize(IMonsterFactory monsterFactory)
        {
            _factory = monsterFactory;
        }
    }
}