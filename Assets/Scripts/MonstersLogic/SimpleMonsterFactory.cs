using Infrastructure.DIContainer;
using UnityEngine;

namespace MonstersLogic
{
    public class SimpleMonsterFactory : IMonsterFactory
    {
        private readonly Monster _monsterPrefab;
        private readonly Transform _moveGoal;

        public SimpleMonsterFactory(Monster monsterPrefab, Transform moveGoal)
        {
            _monsterPrefab = monsterPrefab;
            _moveGoal = moveGoal;
        }

        public Monster CreateMonster(Vector3 position, Quaternion rotation)
        {
            var monster = Object.Instantiate(_monsterPrefab, position, rotation);
            monster.SetMoveTarget(_moveGoal);

            return monster;
        }
    }

    public interface IMonsterFactory : IService
    {
        Monster CreateMonster(Vector3 position, Quaternion rotation);
    }
}