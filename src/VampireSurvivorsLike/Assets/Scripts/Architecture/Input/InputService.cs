using UnityEngine;
using VContainer.Unity;

namespace Architecture.Input
{
    public class InputService : IInitializable, IInputService
    {
        private readonly PlayerInput _input;
        
        public InputService()
        {
            _input = new PlayerInput();
        }
        
        public void Initialize()
        {
            _input.Enable();
            _input.Player.Enable();
        }
        
        public float TurnAxis => _input.Player.Turn.ReadValue<float>();
        public float GasAxis => _input.Player.Gas.ReadValue<float>();
        public Vector2 Aim => _input.Player.Aim.ReadValue<Vector2>();
        public bool IsAttacking => _input.Player.Attack.IsPressed();
    }
}