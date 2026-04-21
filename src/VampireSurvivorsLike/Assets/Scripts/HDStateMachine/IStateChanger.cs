namespace HDStateMachine
{
    public interface IStateChanger
    {
        void ChangeState<TState>() where TState : IState;
    }
}