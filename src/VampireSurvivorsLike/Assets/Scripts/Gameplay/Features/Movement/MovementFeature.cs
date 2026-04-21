using Architecture.Systems;
using Gameplay.Features.Movement.Systems;

namespace Gameplay.Features.Movement
{
    public class MovementFeature : Feature
    {
        public MovementFeature(ISystemFactory factory)
        {
            Add(factory.Create<ProcessMoveVectorSystem>());
            Add(factory.Create<UpdateTransformPositionSystem>());
        }
    }
}