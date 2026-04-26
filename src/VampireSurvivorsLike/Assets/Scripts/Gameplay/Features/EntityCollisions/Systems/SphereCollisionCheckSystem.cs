using System.Collections.Generic;
using Architecture.EntityPhysics;
using Entitas;

namespace Gameplay.Features.EntityCollisions.Systems
{
    public class SphereCollisionCheckSystem : IExecuteSystem
    {
        private readonly IPhysicsService _physics;
        private readonly IGroup<GameEntity> _entities;

        public SphereCollisionCheckSystem(GameContext context, IPhysicsService physics)
        {
            _physics = physics;
            _entities = context.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.PerformingCollisionCheck,
                    GameMatcher.Radius,
                    GameMatcher.WorldPosition,
                    GameMatcher.TargetEntities,
                    GameMatcher.LayerMask,
                    GameMatcher.Alive));
        }
        
        public void Execute()
        {
            foreach (var entity in _entities)
            {
                _physics.SphereOverlap(entity.WorldPosition, entity.Radius, entity.LayerMask, entity.TargetEntities);
                entity.TargetEntities.Remove(entity); // exclude self from list
            }
        }
    }
}