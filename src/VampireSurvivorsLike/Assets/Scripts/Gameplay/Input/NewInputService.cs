using UnityEngine;

namespace Gameplay.Input
{
    public class NewInputService : IInputService
    {
        private readonly PlayerInput _input;

        public NewInputService()
        {
            _input = new PlayerInput();
            _input.Player.Enable();
        }
        
        public Vector2 GetAxisInput()
        {
            return _input.Player.Move.ReadValue<Vector2>().normalized;
        }
    }
}