using System;
using System.Collections.Generic;
using Infrastructure.DIContainer;
using MonstersLogic;
using UnityEngine;

namespace Infrastructure.StateMachine
{
    public class GameStateMachine : MonoBehaviour, IStateSwitcher
    {
        private IGameState _currentState;
        private Dictionary<Type, IGameState> _states;

        public void Initialize(AllServices container, Monster monsterPrefab, Transform moveGoal, MonsterSpawner[] spawners)
        {
            _states = new Dictionary<Type, IGameState>()
            {
                { typeof(BootstrapState), new BootstrapState(this, container, monsterPrefab, moveGoal) },
                { typeof(GameLoopState), new GameLoopState(spawners, container.Single<IMonsterFactory>()) },
                { typeof(EndGameState), new EndGameState() }
            };
        }

        public void Enter<T>() where T : IGameState
        {
            _currentState?.Exit();

            var state = _states[typeof(T)];
            _currentState = state;
            _currentState.Enter();
        }

        public void Exit()
        {
            _currentState?.Exit();
            _currentState = null;
        }

        private void Update()
        {
            _currentState?.Tick();
        }
    }
}