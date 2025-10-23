using UnityEngine;

namespace MonstersLogic
{
    public class MonsterFactory
    {
        private readonly Monster _monsterPrefab;
        private readonly Transform _moveGoal;

        public MonsterFactory(Monster monsterPrefab, Transform moveGoal)
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
}