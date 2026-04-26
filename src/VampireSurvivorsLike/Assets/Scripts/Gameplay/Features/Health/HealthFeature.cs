using Architecture.Systems;
using Gameplay.Features.Health.Systems;

namespace Gameplay.Features.Health
{
    public class HealthFeature : Feature
    {
        public HealthFeature(ISystemFactory factory)
        {
            Add(factory.Create<SetCurrentHealthSystem>());
            Add(factory.Create<CleanupHealthSystem>());
        }
    }
}