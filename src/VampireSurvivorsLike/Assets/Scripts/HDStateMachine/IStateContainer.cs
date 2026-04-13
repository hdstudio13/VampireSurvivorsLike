namespace HDStateMachine
{
    public interface IStateContainer<in TKey>
    {
        void AddState(TKey stateId, IState state);
        void RemoveState(TKey stateId);
    }
}