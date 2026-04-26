using Architecture.TimeManagement;
using Entitas;
using UnityEngine;

namespace Gameplay.Features.Movement.Systems
{
    public class ProcessTargetMoveByDeaccelerationSystem : IExecuteSystem
    {
        private readonly ITimeService _timeService;
        private readonly IGroup<GameEntity> _entities;

        public ProcessTargetMoveByDeaccelerationSystem(GameContext context, ITimeService timeService)
        {
            _timeService = timeService;
            _entities = context.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.Deacceleration,
                    GameMatcher.MoveSpeed)
                .NoneOf(
                    GameMatcher.Moving));
        }
        
        public void Execute()
        {
            foreach (var entity in _entities)
            {
                var moveSpeed = entity.MoveSpeed - entity.Deacceleration * _timeService.DeltaTime;
                moveSpeed = Mathf.Max(moveSpeed, 0);
                entity.ReplaceMoveSpeed(moveSpeed);
            }
        }
    }
}