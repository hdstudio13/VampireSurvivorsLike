using Architecture.TimeManagement;
using Entitas;
using UnityEngine;

namespace Gameplay.Features.Movement.Systems
{
    public class ProcessTargetMoveByAccelerationSystem : IExecuteSystem
    {
        private readonly ITimeService _timeService;
        private readonly IGroup<GameEntity> _entities;

        public ProcessTargetMoveByAccelerationSystem(GameContext context, ITimeService timeService)
        {
            _timeService = timeService;
            _entities = context.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Acceleration,
                    GameMatcher.MoveSpeed,
                    GameMatcher.Moving));
        }
        
        public void Execute()
        {
            foreach (var entity in _entities)
            {
                var moveSpeed = entity.MoveSpeed + entity.Acceleration * _timeService.DeltaTime;
                if (entity.hasMaxMoveSpeed)
                    moveSpeed = Mathf.Min(moveSpeed, entity.MaxMoveSpeed);
                entity.ReplaceMoveSpeed(moveSpeed);
            }
        }
    }
}