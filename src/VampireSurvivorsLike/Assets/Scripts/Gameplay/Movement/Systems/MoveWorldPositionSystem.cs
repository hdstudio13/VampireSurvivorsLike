using Entitas;
using TimeManagement;

namespace Gameplay.Movement.Systems
{
    public class MoveWorldPositionSystem : IExecuteSystem
    {
        private readonly ITimeService _timeService;
        private readonly IGroup<GameEntity> _entities;

        public MoveWorldPositionSystem(GameContext context, ITimeService timeService)
        {
            _timeService = timeService;
            _entities = context.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition, 
                    GameMatcher.MoveDirection, 
                    GameMatcher.Speed)
                );
        }
        
        public void Execute()
        {
            foreach (var entity in _entities)
            {
                entity.ReplaceWorldPosition(entity.WorldPosition + entity.MoveDirection * entity.Speed * _timeService.DeltaTime);
            }
        }
    }
}