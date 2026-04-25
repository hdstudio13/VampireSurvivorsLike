using Architecture.Systems;
using Gameplay.Features.Attacking.Systems;

namespace Gameplay.Features.Attacking
{
    public class AttackFeature : Feature
    {
        public AttackFeature(ISystemFactory factory)
        {
            Add(factory.Create<ProcessProjectileAttackSystem>());
            
            Add(factory.Create<ProcessAttackCooldownSystem>());
            
            Add(factory.Create<CleanupAttackCooldownSystem>());
        }
    }
}