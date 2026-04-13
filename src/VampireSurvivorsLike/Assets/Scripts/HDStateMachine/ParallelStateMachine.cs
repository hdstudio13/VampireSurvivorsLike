using System.Collections.Generic;
using Debug;

namespace HDStateMachine
{
    public class ParallelStateMachine<TKey> : IParallelStateMachine<TKey>
    {
        private readonly Dictionary<TKey, IState> _states = new Dictionary<TKey, IState>();
        private readonly HashSet<TKey> _activeStates = new HashSet<TKey>();
        
        public void AddState(TKey stateId, IState state)
        {
            if (!_states.TryAdd(stateId, state))
            {
                this.LogError($"State with key {stateId} already exists.");
                return;
            }
        }

        public void RemoveState(TKey stateId)
        {
            if (!_states.TryGetValue(stateId, out var state))
            {
                this.LogError($"State with key {stateId} not found.");
                return;
            }
            
            if (_activeStates.Contains(stateId))
            {
                _activeStates.Remove(stateId);
                state.OnExit();
            }
            
            _states.Remove(stateId);
        }

        public void EnterState(TKey key)
        {
            if (_states.TryGetValue(key, out var state))
            {
                if (!_activeStates.Add(key))
                {
                    this.LogError($"State with key {key} is already active.");
                    return;
                }

                state.OnEnter();
            }
            else
            {
                this.LogError($"State with key {key} not found.");
            }
        }

        public void Update(float deltaTime)
        {
            foreach (var activeState in _activeStates)
            {
                _states[activeState].Update(deltaTime);
            }
        }

        public void ExitState(TKey key)
        {
            if (!_states.TryGetValue(key, out var state))
            {
                this.LogError($"State with key {key} not found.");
                return;
            }
            
            if (!_activeStates.Contains(key))
            {
                this.LogError($"State with key {key} is not active.");
                return;
            }
            
            _activeStates.Remove(key);
            state.OnExit();
        }

        public void ExitAll()
        {
            foreach (var activeState in _activeStates)
            {
                _states[activeState].OnExit();
            }
            _activeStates.Clear();
        }
    }
}