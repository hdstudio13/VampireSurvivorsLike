using Architecture.Systems;
using Common;
using Gameplay;
using HDStateMachine;
using Unity.Cinemachine;

namespace Architecture.GameStates
{
    public class GameplayState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly GameplayFeature _gameplayFeature;
        
        public GameplayState
        (
            IStateMachine stateMachine,
            ISystemFactory systemFactory
        )
        {
            _stateMachine = stateMachine;
            _gameplayFeature = new GameplayFeature(systemFactory);
        }
        
        public void OnEnter()
        {
            _gameplayFeature.Initialize();
            _gameplayFeature.ActivateReactiveSystems();
        }

        public void OnExit()
        {
            _gameplayFeature.DeactivateReactiveSystems();
            _gameplayFeature.ClearReactiveSystems();
            _gameplayFeature.Cleanup();
            _gameplayFeature.TearDown();
        }

        public void Update(float deltaTime)
        {
            _gameplayFeature.Execute();
            _gameplayFeature.Cleanup();
        }
    }
}