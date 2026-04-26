using System.Collections.Generic;
using Entitas;
using Gameplay.Features.EntityDestroy;

namespace Gameplay.Features.Effects.Systems
{
    public class CleanupEffectsSystem : ICleanupSystem
    {
        private readonly IGroup<GameEntity> _effects;
        private readonly List<GameEntity> _buffer = new(32);

        public CleanupEffectsSystem(GameContext context)
        {
            _effects = context.GetGroup(GameMatcher.AllOf(GameMatcher.Effect, GameMatcher.Alive));
        }
        
        public void Cleanup()
        {
            foreach (var effect in _effects.GetEntities(_buffer))
            {
                effect.SetDead();
            }
        }
    }
}