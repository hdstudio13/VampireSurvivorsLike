using Entitas;
using UnityEngine;

namespace Gameplay.Input.Systems
{
    public class EmitInputSystem : IExecuteSystem
    {
        private readonly IInputService _input;
        private readonly IGroup<GameEntity> _inputs;

        public EmitInputSystem(GameContext context, IInputService input)
        {
            _input = input;

            _inputs = context.GetGroup(GameMatcher.Input);
        }
        
        public void Execute()
        {
            Vector2 axisInput = _input.GetAxisInput();
            foreach (var input in _inputs)
            {
                if (axisInput != Vector2.zero)
                {
                    input.ReplaceAxisInput(axisInput);
                }
                else if (input.hasAxisInput)
                {
                    input.RemoveAxisInput();
                }
            }
        }
    }
}