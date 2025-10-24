using System;
using Infrastructure.Factory;
using MonstersLogic;

namespace Infrastructure.StateMachine
{
    public class GameLoopState : IGameState
    {
        private readonly MonsterSpawner[] _monsterSpawners;
        private readonly IProjectileFactory _projectileFactory;
        private readonly IMonsterFactory _monsterFactory;

        public GameLoopState(MonsterSpawner[] monsterSpawner, IProjectileFactory projectileFactory, IMonsterFactory monsterFactory)
        {
            _monsterSpawners = monsterSpawner;
            _projectileFactory = projectileFactory;
            _monsterFactory = monsterFactory;
        }
        public void Enter()
        {
            foreach (var tower in UnityEngine.Object.FindObjectsByType<SimpleTower>(UnityEngine.FindObjectsSortMode.None))
            {
                tower.Initialize(_projectileFactory);
            }

            foreach (var monsterSpawner in _monsterSpawners)
            {
                monsterSpawner.Initialize(_monsterFactory);
                monsterSpawner.EnableSpawner();
            }
        }

        public void Exit()
        {
            foreach (var monsterSpawner in _monsterSpawners)
            {
                monsterSpawner.DisableSpawner();
            }
        }

        public void Tick()
        {

        }
    }
}
