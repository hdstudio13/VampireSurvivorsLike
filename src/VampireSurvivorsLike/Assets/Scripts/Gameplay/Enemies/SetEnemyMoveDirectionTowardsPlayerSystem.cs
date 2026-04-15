using Entitas;
using UnityEngine;

namespace Gameplay.Enemies
{
    public class SetEnemyMoveDirectionTowardsPlayerSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _enemies;
        private readonly IGroup<GameEntity> _players;

        public SetEnemyMoveDirectionTowardsPlayerSystem(GameContext context)
        {
            _enemies = context.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Enemy,
                    GameMatcher.WorldPosition,
                    GameMatcher.Speed
                ));
            
            _players = context.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Player,
                    GameMatcher.WorldPosition
                ));
        }
        
        public void Execute()
        {
            foreach (var enemy in _enemies)
            {
                // get nearest player to him
                GameEntity nearestPlayer = null;
                float nearestDistance = float.MaxValue;
                foreach (var player in _players)
                {
                    if (nearestPlayer == null)
                    {
                        nearestPlayer = player;
                        continue;
                    }

                    float distance = Vector3.Distance(enemy.WorldPosition, player.WorldPosition);
                    if (distance < nearestDistance)
                    {
                        nearestPlayer = player;
                        nearestDistance = distance;
                    }
                }

                if (nearestPlayer != null)
                {
                    enemy.ReplaceMoveDirection((nearestPlayer.WorldPosition - enemy.WorldPosition).normalized);
                }
                else if (enemy.hasMoveDirection)
                {
                    enemy.RemoveMoveDirection();
                }
            }
        }
    }
}