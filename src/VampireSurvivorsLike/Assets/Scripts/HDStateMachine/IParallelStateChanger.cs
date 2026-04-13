namespace HDStateMachine
{
    public interface IParallelStateChanger<in TKey> : IStateChanger<TKey>
    {
        void ExitState(TKey key);
        void ExitAll();
    }
}