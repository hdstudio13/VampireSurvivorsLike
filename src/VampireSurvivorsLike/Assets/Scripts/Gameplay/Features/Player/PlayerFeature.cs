using Architecture.Systems;
using Gameplay.Features.Player.Systems;

namespace Gameplay.Features.Player
{
    public class PlayerFeature : Feature
    {
        public PlayerFeature(ISystemFactory systemFactory)
        {
            Add(systemFactory.Create<PlayerInitializationSystem>());
            Add(systemFactory.Create<PlayerEmitMovementSystem>());
        }
    }
}