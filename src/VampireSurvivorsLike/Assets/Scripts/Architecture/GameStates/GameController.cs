using Architecture.TimeManagement;
using Common;
using Debug;
using HDStateMachine;
using VContainer.Unity;

namespace Architecture.GameStates
{
    public class GameController : IInitializable, ITickable
    {
        private readonly StateMachine _stateMachine;
        private readonly IInstantiator _instantiator;
        private readonly ITimeService _timeService;

        public GameController
        (
            IInstantiator instantiator,
            ITimeService timeService
        )
        {
            _stateMachine = new();
            _instantiator = instantiator;
            _timeService = timeService;
        }

        public void Initialize()
        {
            _stateMachine.OnEnterState += state => this.Log($"Entered state: {state.GetType().Name}");
            _stateMachine.AddState(_instantiator.Instantiate<LaunchState>(_stateMachine));
            _stateMachine.AddState(_instantiator.Instantiate<GameplayState>(_stateMachine));
            _stateMachine.ChangeState<LaunchState>();
        }

        public void Tick()
        {
            _stateMachine.Update(_timeService.DeltaTime);
        }
    }
}