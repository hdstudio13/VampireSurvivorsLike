using Architecture.Systems;
using Gameplay.Features.EntityCollisions.Systems;

namespace Gameplay.Features.EntityCollisions
{
    public class CollisionCheckFeature : Feature
    {
        public CollisionCheckFeature(ISystemFactory factory)
        {
            Add(factory.Create<SphereCollisionCheckSystem>());
        }
    }
}