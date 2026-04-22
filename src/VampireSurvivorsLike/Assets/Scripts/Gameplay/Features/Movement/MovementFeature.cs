using Architecture.Systems;
using Gameplay.Features.Movement.Systems;

namespace Gameplay.Features.Movement
{
    public class MovementFeature : Feature
    {
        public MovementFeature(ISystemFactory factory)
        {
            Add(factory.Create<SetUpMoveVectorSystem>());
            
            Add(factory.Create<ProcessTargetMoveByAccelerationSystem>());
            Add(factory.Create<ProcessTargetMoveByDeaccelerationSystem>());
            Add(factory.Create<StraightForwardProcessingOfTargetMoveSystem>());
            
            Add(factory.Create<ApplyMoveToWorldPosition>());
            Add(factory.Create<UpdateTransformPositionSystem>());
        }
    }
}