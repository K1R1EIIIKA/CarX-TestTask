using System;
using Infrastructure.DIContainer;
using MonstersLogic;
using UnityEngine;

namespace Infrastructure.StateMachine
{
    public class BootstrapState : IGameState
    {
        private readonly AllServices _allServices;
        private readonly IStateSwitcher _stateSwitcher;
        private readonly Monster _monsterPrefab;
        private readonly Transform _moveGoal;

        public BootstrapState(IStateSwitcher stateSwitcher, AllServices allServices, Monster monsterPrefab, Transform moveGoal)
        {
            _allServices = allServices;
            _stateSwitcher = stateSwitcher;
            _monsterPrefab = monsterPrefab;
            _moveGoal = moveGoal;

            RegisterServices();
        }


        private void RegisterServices()
        {
            _allServices.RegisterSingle<IMonsterFactory>(new SimpleMonsterFactory(_monsterPrefab, _moveGoal));
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