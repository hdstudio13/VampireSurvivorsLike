using Gameplay.Movement.Systems;
using TimeManagement;

namespace Gameplay.Movement
{
    public class MovementFeature : Feature
    {
        public MovementFeature(GameContext context, ITimeService timeService)
        {
            Add(new MoveWorldPositionSystem(context, timeService));
            Add(new UpdateTransformPositionSystem(context));
        }
    }
}