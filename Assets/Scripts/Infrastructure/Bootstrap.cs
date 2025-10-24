using Infrastructure.DIContainer;
using Infrastructure.StateMachine;
using MonstersLogic;
using UnityEngine;

namespace Infrastructure
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private Monster _monsterPrefab;
        [SerializeField] private Transform _moveGoal;
        [SerializeField] private MonsterSpawner[] _monsterSpawners;

        private GameStateMachine _gameStateMachine;

        private void Awake()
        {
            _gameStateMachine = gameObject.AddComponent<GameStateMachine>();
            _gameStateMachine.Initialize(AllServices.Container, _monsterPrefab, _moveGoal, _monsterSpawners);
            _gameStateMachine.Enter<BootstrapState>();
        }
    }
}