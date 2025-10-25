using CanonLogic;
using Infrastructure.Asset;
using Infrastructure.DIContainer;
using Infrastructure.Factory;
using Infrastructure.ObjectPooling;
using UnityEngine;

namespace Infrastructure.StateMachine
{
    public class BootstrapState : IGameState
    {
        private readonly AllServices _allServices;
        private readonly IStateSwitcher _stateSwitcher;
        private readonly Transform _moveGoal;

        public BootstrapState(IStateSwitcher stateSwitcher, AllServices allServices, Transform moveGoal)
        {
            _allServices = allServices;
            _stateSwitcher = stateSwitcher;
            _moveGoal = moveGoal;

            RegisterServices();
        }


        private void RegisterServices()
        {
            _allServices.RegisterSingle<IAssetDatabase>(new AssetDatabase());
            var assetDatabase = _allServices.Single<IAssetDatabase>();

            _allServices.RegisterSingle(new SimpleProjectilePool(assetDatabase.ProjectileConfig(ProjectileType.Simple).ProjectilePrefab, 10));
            _allServices.RegisterSingle(new CannonProjectilePool(assetDatabase.ProjectileConfig(ProjectileType.Cannon).ProjectilePrefab, 10));
            _allServices.RegisterSingle(new MortarProjectilePool(assetDatabase.ProjectileConfig(ProjectileType.Mortar).ProjectilePrefab, 10));

            _allServices.RegisterSingle<IMonsterFactory>(new SimpleMonsterFactory(assetDatabase.MonsterConfig, _moveGoal));
            _allServices.RegisterSingle<IProjectileFactory>(new ProjectileFactory(
                _allServices.Single<SimpleProjectilePool>(),
                _allServices.Single<CannonProjectilePool>(),
                _allServices.Single<MortarProjectilePool>(),
                _allServices.Single<IAssetDatabase>()));
        }

        public void Enter()
        {
            _stateSwitcher.Enter<GameLoopState>();
        }

        public void Exit()
        {

        }

        public void Tick()
        {

        }
    }
}
