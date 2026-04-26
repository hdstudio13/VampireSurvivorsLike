using Entitas;

namespace Gameplay.Features.EntityCollisions.Systems
{
    public class CleanupCollisionSystem : ICleanupSystem
    {
        private readonly IGroup<GameEntity> _entities;

        public CleanupCollisionSystem(GameContext context)
        {
            _entities = context.GetGroup(GameMatcher.TargetEntities);
        }
        
        public void Cleanup()
        {
            foreach (var entity in _entities)
            {
                entity.TargetEntities.Clear();
            }
        }
    }
}