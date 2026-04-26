using System.Collections.Generic;
using Architecture.TimeManagement;
using Entitas;
using NUnit.Framework;

namespace Gameplay.Features.Movement.Systems
{
    public class ApplyMoveToWorldPosition : IExecuteSystem
    {
        private readonly ITimeService _time;
        private IGroup<GameEntity> _movers;

        public ApplyMoveToWorldPosition(GameContext context, ITimeService time)
        {
            _time = time;
            _movers = context.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.MoveDirection,
                    GameMatcher.MoveSpeed)
            );
        }
        
        public void Execute()
        {
            foreach (var mover in _movers)
            {
                mover.ReplaceWorldPosition(mover.WorldPosition + mover.MoveDirection * mover.MoveSpeed * _time.DeltaTime);
            }
        }
    }
}