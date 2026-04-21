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
        
        public Vector2 MoveAxis => _input.Player.Move.ReadValue<Vector2>();
    }
}