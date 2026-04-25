using Architecture.TimeManagement;
using Entitas;
using UnityEngine;

namespace Gameplay.Features.Projectiles.Systems
{
    public class MoveProjectilesForwardSystem : IExecuteSystem
    {
        private readonly ITimeService _timeService;
        private IGroup<GameEntity> _projectiles;

        public MoveProjectilesForwardSystem(GameContext context, ITimeService timeService)
        {
            _timeService = timeService;
            _projectiles = context.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Projectile,
                    GameMatcher.WorldRotation,
                    GameMatcher.MoveSpeed));
        }
        
        public void Execute()
        {
            foreach (var projectile in _projectiles)
            {
                projectile.ReplaceMoveVector(projectile.WorldRotation * Vector3.forward * projectile.MoveSpeed * _timeService.DeltaTime);
            }
        }
    }
}