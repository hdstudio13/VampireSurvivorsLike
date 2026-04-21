namespace HDStateMachine
{
    public interface IStateMachine : IStateContainer, IStateChanger
    {
        public void Update(float deltaTime);
    }
}