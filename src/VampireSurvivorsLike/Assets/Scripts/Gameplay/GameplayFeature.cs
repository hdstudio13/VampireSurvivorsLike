using Architecture;
using Architecture.Systems;
using Gameplay.Features.Movement;
using Gameplay.Features.Player;
using Gameplay.Features.View;

namespace Gameplay
{
    public class GameplayFeature : Feature
    {
        public GameplayFeature(ISystemFactory factory)
        {
            Add(factory.Create<PlayerFeature>());
            Add(factory.Create<MovementFeature>());
            Add(factory.Create<ViewFeature>());
        }
    }
}