using Architecture.Systems;
using Gameplay.Features.Rotation.Systems;

namespace Gameplay.Features.Rotation
{
    public class RotationFeature : Feature
    {
        public RotationFeature(ISystemFactory factory)
        {
            Add(factory.Create<AlterRotationByDeltaRotationSystem>());
            Add(factory.Create<UpdateTransformRotationSystem>());
        }
    }
}