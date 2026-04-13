namespace HDStateMachine
{
    public interface IState
    {
        public virtual void OnEnter() { }
        public virtual void OnExit() { }
        public virtual void Update(float deltaTime) { }
    }
}
