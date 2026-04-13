using Gameplay.Input.Systems;

namespace Gameplay.Input
{
    public class InputFeature : Feature
    {
        public InputFeature(GameContext context, IInputService inputService)
        {
            Add(new InitializeInputSystem(context));
            Add(new EmitInputSystem(context, inputService));
        }
    }
}