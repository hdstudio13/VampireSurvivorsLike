using Architecture.Configs;
using AssetManagement;
using Common;
using HDStateMachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace Architecture.GameStates
{
    public class GameplayInitializationState : IState
    {
        private readonly IStateMachine _gameStateMachine;
        private readonly RootLifetimeScope _rootScope;
        private readonly IConfigProvider _configProvider;

        public GameplayInitializationState
        (
            IStateMachine gameStateMachine,
            RootLifetimeScope rootScope,
            IConfigProvider configProvider
        )
        {
            _gameStateMachine = gameStateMachine;
            _rootScope = rootScope;
            _configProvider = configProvider;
        }
        
        public void OnEnter()
        {
            var gameConfig = _configProvider.Get<GameConfig>();
            var gameplayScope = _rootScope.CreateChildFromPrefab(gameConfig.GameplayScopePrefab);
            gameplayScope.transform.SetParent(null);
            SceneManager.MoveGameObjectToScene(gameplayScope.gameObject, SceneManager.GetActiveScene());
            IInstantiator instantiator = gameplayScope.Container.Resolve<IInstantiator>();
            _gameStateMachine.AddState(instantiator.Instantiate<GameplayState>(_gameStateMachine));
            _gameStateMachine.ChangeState<GameplayState>();
        }

        public void OnExit()
        {
            
        }

        public void Update(float deltaTime)
        {
            
        }
    }
}