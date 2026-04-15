using Architecture;
using Gameplay.Input.Systems;

namespace Gameplay.Input
{
    public class InputFeature : Feature
    {
        public InputFeature(ISystemFactory factory)
        {
            Add(factory.Create<InitializeInputSystem>());
            Add(factory.Create<EmitInputSystem>());
        }
    }
}