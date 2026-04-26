using Architecture.Systems;
using Gameplay.Features.Effects.Systems;
using Gameplay.Features.Effects.Systems.Damage;

namespace Gameplay.Features.Effects
{
    public class EffectFeature : Feature
    {
        public EffectFeature(ISystemFactory factory)
        {
            Add(factory.Create<ProcessDamageEffectsSystem>());
            Add(factory.Create<CleanupEffectsSystem>());
        }
    }
}