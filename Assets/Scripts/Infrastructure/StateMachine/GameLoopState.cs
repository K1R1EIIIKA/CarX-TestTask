using System;
using CanonLogic;
using Infrastructure.Asset;
using Infrastructure.Factory;
using MonstersLogic;

namespace Infrastructure.StateMachine
{
    public class GameLoopState : IGameState
    {
        private readonly MonsterSpawner[] _monsterSpawners;
        private readonly IProjectileFactory _projectileFactory;
        private readonly IMonsterFactory _monsterFactory;
        private readonly IAssetDatabase _assetDatabase;

        public GameLoopState(MonsterSpawner[] monsterSpawner, IProjectileFactory projectileFactory, IMonsterFactory monsterFactory, IAssetDatabase assetDatabase)
        {
            _monsterSpawners = monsterSpawner;
            _projectileFactory = projectileFactory;
            _monsterFactory = monsterFactory;
            _assetDatabase = assetDatabase;
        }
        
        public void Enter()
        {
            foreach (var tower in UnityEngine.Object.FindObjectsByType<BaseTower>(UnityEngine.FindObjectsSortMode.None))
            {
                tower.Initialize(_projectileFactory, _assetDatabase);
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
