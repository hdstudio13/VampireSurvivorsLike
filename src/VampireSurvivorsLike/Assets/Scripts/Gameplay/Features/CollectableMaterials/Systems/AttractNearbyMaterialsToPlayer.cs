using Entitas;
using UnityEngine;

namespace Gameplay.Features.CollectableMaterials.Systems
{
    public class AttractNearbyMaterialsToPlayer : IExecuteSystem
    {
        private IGroup<GameEntity> _materials;
        private readonly IGroup<GameEntity> _players;

        public AttractNearbyMaterialsToPlayer(GameContext context)
        {
            _materials = context.GetGroup(GameMatcher
                .AllOf(GameMatcher.Material, GameMatcher.WorldPosition, GameMatcher.MoveSpeed, GameMatcher.Alive));
            _players = context.GetGroup(GameMatcher
                .AllOf(GameMatcher.Player, GameMatcher.WorldPosition, GameMatcher.MoveSpeed).NoneOf(GameMatcher.Turret));
        }
        
        public void Execute()
        {
            foreach (var player in _players)
            {
                foreach (var material in _materials)
                {
                    if (Vector3.Distance(material.WorldPosition, player.WorldPosition) < 5f)
                    {
                        var direction = (player.WorldPosition - material.WorldPosition).normalized;
                        material.ReplaceMoveDirection(direction);
                        material.isMoving = true;
                    }
                    else
                    {
                        material.isMoving = false;
                    }
                }
            }
        }
    }
}