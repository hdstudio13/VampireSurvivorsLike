using Architecture.Systems;
using Gameplay.Features.EntityDestroy.Systems;

namespace Gameplay.Features.EntityDestroy
{
    public class DestroyFeature : Feature
    {
        public DestroyFeature(ISystemFactory factory)
        {
            Add(factory.Create<ProcessDestroyTimerSystem>());
            Add(factory.Create<ValidateDestroyingSystem>());
            Add(factory.Create<CleanUpDestroyedEntitiesSystem>());
        }
    }
}