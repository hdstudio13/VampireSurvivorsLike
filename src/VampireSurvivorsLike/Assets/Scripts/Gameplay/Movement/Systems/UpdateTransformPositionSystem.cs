using Entitas;

namespace Gameplay.Movement.Systems
{
    public class UpdateTransformPositionSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _targets;

        public UpdateTransformPositionSystem(GameContext context)
        {
            _targets = context.GetGroup(GameMatcher
                .AllOf(
                    GameMatcher.WorldPosition,
                    GameMatcher.Transform
                    )
            );
        }
        
        public void Execute()
        {
            foreach (var target in _targets)
            {
                target.Transform.position = target.WorldPosition;
            }
        }
    }
}