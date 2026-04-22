using Entitas;
using UnityEngine;

namespace Gameplay.Features.Movement.Systems
{
    public class UpdateTransformPositionSystem : IExecuteSystem
    {
        private IGroup<GameEntity> _entities;

        public UpdateTransformPositionSystem(GameContext context)
        {
            _entities = context.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.Transform)
            );
        }
        
        public void Execute()
        {
            foreach (var mover in _entities)
            {
                mover.Transform.position = mover.WorldPosition;
            }
        }
    }
}