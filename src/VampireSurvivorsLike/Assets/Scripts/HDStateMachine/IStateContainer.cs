namespace HDStateMachine
{
    public interface IStateContainer
    {
        void AddState(IState state);
        void RemoveState<TState>() where TState : IState;
    }
}