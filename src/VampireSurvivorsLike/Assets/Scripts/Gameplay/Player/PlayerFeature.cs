using Gameplay.Player.Systems;

namespace Gameplay.Player
{
    public class PlayerFeature : Feature
    {
        public PlayerFeature(GameContext context)
        {
            Add(new UpdatePlayerMoveDirectionByInputSystem(context));
        }
    }
}