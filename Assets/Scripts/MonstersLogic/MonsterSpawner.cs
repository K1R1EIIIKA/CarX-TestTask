using System;
using UnityEngine;

namespace MonstersLogic
{
    public class MonsterSpawner : MonoBehaviour
    {
        [SerializeField] private float _interval = 3;
        [SerializeField] private Transform _moveTarget;
        [SerializeField] private Monster _monsterPrefab;

        private float _lastSpawn = Mathf.NegativeInfinity;

        private MonsterFactory _factory;

        private void Awake()
        {
            _factory = new MonsterFactory(_monsterPrefab, _moveTarget);
        }

        void Update()
        {
            if (Time.time > _lastSpawn + _interval)
            {
                _factory.CreateMonster(transform.position, Quaternion.identity);
                _lastSpawn = Time.time;
            }
        }
    }
}