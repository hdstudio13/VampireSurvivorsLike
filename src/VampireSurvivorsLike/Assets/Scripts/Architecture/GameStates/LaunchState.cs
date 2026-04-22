using HDStateMachine;

namespace Architecture.GameStates
{
    public class LaunchState : IState
    {
        private readonly IStateChanger _stateChanger;

        public LaunchState
        (
            IStateChanger stateChanger
        )
        {
            _stateChanger = stateChanger;
        }
        
        public void OnEnter()
        {
            _stateChanger.ChangeState<GameplayInitializationState>();
        }

        public void OnExit()
        {
            
        }

        public void Update(float deltaTime)
        {
            
        }
    }
}