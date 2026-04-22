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

        public event Action<IState> OnStateAdded;
        public event Action<IState> OnStateRemoved;
        public event Action<IState> OnEnterState;
        public event Action<IState> OnExitState;
        public event Action<IState> OnUpdate;
        
        public void AddState(IState state)
        {
            _states.Add(state.GetType(), state);
            OnStateAdded?.Invoke(state);
        }

        public void RemoveState<TState>() where TState : IState
        {
            if (_currentState != null && _currentState.GetType() == typeof(TState))
            {
                this.LogError($"Cannot remove state {typeof(TState).Name} because it is currently active.");
                return;
            }
            
            _states.TryGetValue(typeof(TState), out var state);
            _states.Remove(typeof(TState));
            OnStateRemoved?.Invoke(state);
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