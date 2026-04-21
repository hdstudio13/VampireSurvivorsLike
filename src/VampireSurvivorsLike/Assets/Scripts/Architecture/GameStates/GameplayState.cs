using Architecture.Systems;
using Common;
using Gameplay;
using HDStateMachine;

namespace Architecture.GameStates
{
    public class GameplayState : IState
    {
        private readonly IStateChanger _stateChanger;
        private readonly GameplayFeature _gameplayFeature;
        
        public GameplayState
        (
            IStateChanger stateChanger,
            ISystemFactory systemFactory
        )
        {
            _stateChanger = stateChanger;
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