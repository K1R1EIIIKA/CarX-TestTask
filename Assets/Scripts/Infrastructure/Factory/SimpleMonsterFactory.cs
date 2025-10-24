using Infrastructure.Configs;
using MonstersLogic;
using UnityEngine;

namespace Infrastructure.Factory
{
    public class SimpleMonsterFactory : IMonsterFactory
    {
        private readonly MonsterConfig _monsterConfig;
        private readonly Transform _moveGoal;

        public SimpleMonsterFactory(MonsterConfig monster, Transform moveGoal)
        {
            _monsterConfig = monster;
            _moveGoal = moveGoal;
        }

        public Monster CreateMonster(Vector3 position, Quaternion rotation)
        {
            var monster = Object.Instantiate(_monsterConfig.MonsterPrefab, position, rotation);
            monster.SetMoveTarget(_moveGoal);
            monster.Initialize(_monsterConfig.Speed, _monsterConfig.Health);

            return monster;
        }
    }
}