using System;
using MonstersLogic;

namespace Infrastructure.StateMachine
{
    public class GameLoopState : IGameState
    {
        private readonly MonsterSpawner[] _monsterSpawners;
        private readonly IMonsterFactory _monsterFactory;

        public GameLoopState(MonsterSpawner[] monsterSpawner, IMonsterFactory monsterFactory)
        {
            _monsterSpawners = monsterSpawner;
            _monsterFactory = monsterFactory;
        }
        public void Enter()
        {
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