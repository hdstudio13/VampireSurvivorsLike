using System.Collections.Generic;
using Entitas;

namespace Gameplay.Features.Health.Systems
{
    public class SetCurrentHealthSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _healths;
        private readonly List<GameEntity> _buffer = new(32);

        public SetCurrentHealthSystem(GameContext context)
        {
            _healths = context.GetGroup(GameMatcher
                .AllOf(GameMatcher.Alive, GameMatcher.Health)
                .NoneOf(GameMatcher.CurrentHealth));
        }
        
        public void Execute()
        {
            foreach (var entity in _healths.GetEntities(_buffer))
            {
                entity.ReplaceCurrentHealth(entity.Health);
            }
        }
    }
}