namespace HDStateMachine
{
    public interface IStateChanger<in TKey>
    {
        void EnterState(TKey key);
    }
}