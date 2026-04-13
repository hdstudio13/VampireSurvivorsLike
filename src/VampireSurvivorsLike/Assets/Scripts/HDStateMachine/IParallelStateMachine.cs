namespace HDStateMachine
{
    public interface IParallelStateMachine<in TKey> : IStateMachine<TKey>, IParallelStateChanger<TKey>
    {
    }
}