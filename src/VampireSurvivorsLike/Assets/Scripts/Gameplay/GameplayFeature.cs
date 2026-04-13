using Gameplay.Input;
using Gameplay.Movement;
using Gameplay.Player;
using TimeManagement;

namespace Gameplay
{
    public class GameplayFeature : Feature
    {
        public GameplayFeature(GameContext context, ITimeService timeService, IInputService inputService)
        {
            Add(new InputFeature(context, inputService));
            Add(new MovementFeature(context, timeService));
            Add(new PlayerFeature(context));
        }
    }
}