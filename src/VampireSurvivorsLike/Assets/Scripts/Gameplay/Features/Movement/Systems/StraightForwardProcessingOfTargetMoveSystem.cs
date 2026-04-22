using Entitas;

namespace Gameplay.Features.Movement.Systems
{
    public class StraightForwardProcessingOfTargetMoveSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _entities;

        public StraightForwardProcessingOfTargetMoveSystem(GameContext context)
        {
            _entities = context.GetGroup(GameMatcher
                .AllOf(GameMatcher.TargetMoveVector)
                .NoneOf(GameMatcher.Acceleration, GameMatcher.Deacceleration));
        }
        
        public void Execute()
        {
            foreach (var entity in _entities)
            {
                entity.ReplaceMoveVector(entity.TargetMoveVector);
            }
        }
    }
}