using Architecture;
using Gameplay.Movement.Systems;
using TimeManagement;

namespace Gameplay.Movement
{
    public class MovementFeature : Feature
    {
        public MovementFeature(ISystemFactory factory)
        {
            Add(factory.Create<MoveWorldPositionSystem>());
            Add(factory.Create<UpdateTransformPositionSystem>());
        }
    }
}