using System.Collections.Generic;
using Entitas;
using Gameplay.Features.EntityDestroy;

namespace Gameplay.Features.Health.Systems
{
    public class CleanupHealthSystem : ICleanupSystem
    {
        private readonly IGroup<GameEntity> _healths;
        private readonly List<GameEntity> _buffer = new(32);

        public CleanupHealthSystem(GameContext context)
        {
            _healths = context.GetGroup(GameMatcher.AllOf(GameMatcher.CurrentHealth, GameMatcher.Alive));
        }
        
        public void Cleanup()
        {
            foreach (var entity in _healths.GetEntities(_buffer))
            {
                if (entity.CurrentHealth <= 0)
                {
                    entity.SetDead();
                }
            }
        }
    }
}