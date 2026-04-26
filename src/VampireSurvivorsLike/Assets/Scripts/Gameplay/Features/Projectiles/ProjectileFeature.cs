using Architecture.Systems;
using Gameplay.Features.Projectiles.Systems;

namespace Gameplay.Features.Projectiles
{
    public class ProjectileFeature : Feature
    {
        public ProjectileFeature(ISystemFactory factory)
        {
            Add(factory.Create<MoveProjectilesForwardSystem>());
            Add(factory.Create<ProjectileCollisionHandleSystem>());
        }
    }
}