namespace HDStateMachine
{
    public interface IStateMachine<in TKey> : IStateContainer<TKey>, IStateChanger<TKey>
    {
        public void Update(float deltaTime);
    }
}