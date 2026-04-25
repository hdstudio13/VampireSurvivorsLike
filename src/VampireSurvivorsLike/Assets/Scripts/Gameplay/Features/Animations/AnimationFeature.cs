using Architecture.Systems;
using Gameplay.Features.Animations.Systems;

namespace Gameplay.Features.Animations
{
    public class AnimationFeature : Feature
    {
        public AnimationFeature(ISystemFactory factory)
        {
            Add(factory.Create<AnimateAttackSystem>());
        }
    }
}