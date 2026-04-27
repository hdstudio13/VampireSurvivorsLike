using System.Collections.Generic;
using Architecture.CameraManagement;
using Architecture.EntityPhysics;
using Entitas;

namespace Gameplay.Features.EntityCollisions.Systems
{
    public class SphereCollisionCheckSystem : IExecuteSystem
    {
        private readonly IPhysicsService _physics;
        private readonly ICameraService _cameraService;
        private readonly IGroup<GameEntity> _entities;

        public SphereCollisionCheckSystem(GameContext context, IPhysicsService physics, ICameraService cameraService)
        {
            _physics = physics;
            _cameraService = cameraService;
            _entities = context.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.PerformingCollisionCheck,
                    GameMatcher.Radius,
                    GameMatcher.WorldPosition,
                    GameMatcher.TargetEntities,
                    GameMatcher.LayerMask,
                    GameMatcher.Alive)
                .NoneOf(GameMatcher.CollisionCooldownTimer));
        }
        
        public void Execute()
        {
            foreach (var entity in _entities)
            {
                if (entity.isCullingCollision && !_cameraService.IsOnScreen(entity.WorldPosition, 0.5f))
                    continue;
                
                _physics.SphereOverlap(entity.WorldPosition, entity.Radius, entity.LayerMask, entity.TargetEntities);
                entity.TargetEntities.Remove(entity); // exclude self from list
            }
        }
    }
}