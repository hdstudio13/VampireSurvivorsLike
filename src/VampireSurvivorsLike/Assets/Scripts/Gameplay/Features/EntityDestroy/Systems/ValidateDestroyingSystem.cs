

using System.Collections.Generic;
using Entitas;

namespace Gameplay.Features.EntityDestroy.Systems
{
    public class ValidateDestroyingSystem : IExecuteSystem 
    {
        private IGroup<GameEntity> _validEntities;
        private List<GameEntity> _buffer = new(32);

        public ValidateDestroyingSystem(GameContext context)
        {
            _validEntities = context.GetGroup(GameMatcher
                .AllOf(GameMatcher.Destroying)
                .NoneOf(GameMatcher.View, GameMatcher.MaterialType, GameMatcher.Destroyed));
        }
        
        public void Execute()
        {
            foreach (var entity in _validEntities.GetEntities(_buffer))
            {
                entity.isDestroyed = true;
            }
        }
    }
}