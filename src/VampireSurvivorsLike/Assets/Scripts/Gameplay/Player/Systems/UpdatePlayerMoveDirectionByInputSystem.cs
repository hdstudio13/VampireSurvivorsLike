using Entitas;
using UnityEngine;

namespace Gameplay.Player.Systems
{
    public class UpdatePlayerMoveDirectionByInputSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _players;
        private readonly IGroup<GameEntity> _inputs;

        public UpdatePlayerMoveDirectionByInputSystem(GameContext context)
        {
            _players = context.GetGroup(GameMatcher.Player);
            _inputs = context.GetGroup(GameMatcher.AllOf(GameMatcher.Input));
        }
        
        public void Execute()
        {
            foreach (var player in _players)
                foreach (var input in _inputs)
                    if (input.hasAxisInput)
                        player.ReplaceMoveDirection(new Vector3(input.AxisInput.x, 0, input.AxisInput.y));
                    else if (player.hasMoveDirection)
                        player.RemoveMoveDirection();
                        
        }
    }
}