using System.Collections.Generic;
using Entitas;

namespace Gameplay.Features.Rotation.Systems
{
    public class AlterRotationByDeltaRotationSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _rotations;
        private readonly List<GameEntity> _buffer = new(128);

        public AlterRotationByDeltaRotationSystem(GameContext gameContext)
        {
            _rotations = gameContext.GetGroup(GameMatcher.AllOf(
                GameMatcher.WorldRotation,
                GameMatcher.DeltaRotation));
        }
        
        public void Execute()
        {
            foreach (var entity in _rotations.GetEntities(_buffer))
            {
                entity.ReplaceWorldRotation(entity.WorldRotation * entity.DeltaRotation);
            }
        }
    }
}