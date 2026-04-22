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
                    GameMatcher.TargetMoveVector,
                    GameMatcher.Acceleration,
                    GameMatcher.MoveVector));
        }
        
        public void Execute()
        {
            foreach (var entity in _entities)
            {
                if (entity.TargetMoveVector.magnitude > entity.MoveVector.magnitude)
                {
                    entity.ReplaceMoveVector(Vector3.MoveTowards(entity.MoveVector, entity.TargetMoveVector, entity.Acceleration * _timeService.DeltaTime));
                }
            }
        }
    }
}