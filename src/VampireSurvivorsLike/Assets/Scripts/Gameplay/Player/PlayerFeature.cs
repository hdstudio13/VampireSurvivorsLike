using Architecture;
using Gameplay.Player.Systems;

namespace Gameplay.Player
{
    public class PlayerFeature : Feature
    {
        public PlayerFeature(ISystemFactory factory)
        {
            Add(factory.Create<UpdatePlayerMoveDirectionByInputSystem>());
        }
    }
}