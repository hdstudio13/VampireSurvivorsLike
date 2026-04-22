using System.Collections.Generic;
using Entitas;

namespace Gameplay.Features.Rotation.Systems
{
    public class UpdateTransformRotationSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _entities;
        private readonly List<GameEntity> _buffer = new(128);

        public UpdateTransformRotationSystem(GameContext gameContext)
        {
            _entities = gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.WorldRotation, GameMatcher.Transform));
        }
        
        public void Execute()
        {
            foreach (var entity in _entities.GetEntities(_buffer))
            {
                entity.Transform.rotation = entity.WorldRotation;
            }
        }
    }
}