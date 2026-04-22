using System.Collections.Generic;
using Entitas;

namespace Gameplay.Features.EntityDestroy.Systems
{
    public class CleanUpDestroyedEntitiesSystem : ICleanupSystem
    {
        private IGroup<GameEntity> _destroyedEntities;
        private List<GameEntity> _buffer = new(32);

        public CleanUpDestroyedEntitiesSystem(GameContext context)
        {
            _destroyedEntities = context.GetGroup(GameMatcher.Destroyed);
        }
        
        public void Cleanup()
        {
            foreach (var entity in _destroyedEntities.GetEntities(_buffer))
            {
                entity.Destroy();
            }
        }
    }
}