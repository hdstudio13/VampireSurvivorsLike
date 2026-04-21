using System.Collections.Generic;
using Entitas;
using NUnit.Framework;

namespace Gameplay.Features.Movement.Systems
{
    public class ProcessMoveVectorSystem : IExecuteSystem
    {
        private IGroup<GameEntity> _movers;
        private List<GameEntity> _buffer = new(128);

        public ProcessMoveVectorSystem(GameContext context)
        {
            _movers = context.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.MoveVector)
            );
        }
        
        public void Execute()
        {
            foreach (var mover in _movers.GetEntities(_buffer))
            {
                mover.ReplaceWorldPosition(mover.WorldPosition + mover.MoveVector);
                mover.RemoveMoveVector();
            }
        }
    }
}