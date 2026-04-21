using System;
using System.Collections.Generic;
using Debug;

namespace HDStateMachine
{
    public class StateMachine : IStateMachine
    {
        private readonly Dictionary<Type, IState> _states = new();
        private IState _currentState;
        private IState _nextState;
        
        public event Action<IState> OnEnterState;
        public event Action<IState> OnExitState;
        public event Action<IState> OnUpdate;
        
        public void AddState(IState state)
        {
            _states.Add(state.GetType(), state);
        }

        public void RemoveState<TState>() where TState : IState
        {
            _states.Remove(typeof(TState));
        }

        public void ChangeState<TState>() where TState : IState
        {
            _nextState = _states.GetValueOrDefault(typeof(TState));
        }

        public void Update(float deltaTime)
        {
            _currentState?.Update(deltaTime);
            OnUpdate?.Invoke(_currentState);

            if (_nextState != _currentState)
            {
                PerformStateSwitch();
            }
        }

        private void PerformStateSwitch()
        {
            if (_currentState != null)
            {
                _currentState.OnExit();
                OnExitState?.Invoke(_currentState);
            }
            
            if (_nextState != null)
            {
                _currentState = _nextState;
                _currentState.OnEnter();
            }
            else
            {
                _currentState = null;
            }
            OnEnterState?.Invoke(_currentState);
        }
    }
}